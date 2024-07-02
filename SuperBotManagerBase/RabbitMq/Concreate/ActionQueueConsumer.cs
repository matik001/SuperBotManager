using Microsoft.Extensions.Logging;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Concreate
{
    public abstract class ActionQueueConsumer : IQueueConsumer<ActionQueueMessage>
    {
        readonly ILogger<ActionQueueConsumer> logger;
        IAppUnitOfWork uow;
        IActionService actionService;

        protected ActionQueueConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService)
        {
            this.logger = logger;
            this.uow = uow;
            this.actionService = actionService;
        }
        private async Task WaitUntilCancelled(int actionId, CancellationToken cancelToken)
        {
            while(true)
            {
                var action = await uow.ActionRepository.GetById(actionId);
                if(action.ActionStatus == ActionStatus.Canceled)
                    return;
                await Task.Delay(1000, cancelToken);
                if(cancelToken.IsCancellationRequested)
                    return;
            }
        }
        public async Task ConsumeAsync(ActionQueueMessage message)
        {
            try
            {
                var action = message.Action;
                logger.LogInformation($"Processing: {action.Id} ({action.ActionExecutor.ActionExecutorName} - {action.ActionExecutor.ActionDefinition.ActionDefinitionName})");


                var dbAction = await uow.ActionRepository.GetById(action.Id);
                if(dbAction.ActionStatus == ActionStatus.Canceled)
                {
                    logger.LogInformation($"Action {action.Id} is canceled. Skipping execution.");
                    return;
                }

                action.ActionStatus = ActionStatus.InProgress;
                await uow.ActionRepository.Update(action);
                await uow.SaveChangesAsync();

                action.ActionData = await ActionSchema.Decrypt(uow, action.ActionData, action.ActionExecutor.ActionDefinition.ActionDataSchema);
                var cts = new CancellationTokenSource();

                var cancelTask = WaitUntilCancelled(action.Id, cts.Token);
                var executeTask = ExecuteAsync(action, cts.Token);
                await Task.WhenAny(cancelTask, executeTask);

                if(cancelTask.IsCompleted)
                {
                    cts.Cancel();
                    await executeTask;
                    logger.LogInformation($"Action {action.Id} is canceled. Skipping execution.");
                    return;
                }
                var output = await executeTask;

                logger.LogInformation($"Executed: {action.Id} ({action.ActionExecutor.ActionExecutorName} - {action.ActionExecutor.ActionDefinition.ActionDefinitionName})");
                action.ActionStatus = ActionStatus.Finished;
                action.ActionData.Output = output;

                action.ActionData = await ActionSchema.Encrypt(uow, action.ActionData, action.ActionExecutor.ActionDefinition.ActionDataSchema);

                await uow.ActionRepository.Update(action);

                await uow.SaveChangesAsync();

                if(action.ActionExecutor.ActionExecutorOnFinishId != null)
                    await actionService.EnqueueExecution(action.ActionExecutor.ActionExecutorOnFinishId.Value, action.RunStartType, action);

                await uow.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                /// todo We can use transaction here to rollback all changes
                uow.UntrackAll();
                logger.LogError(ex, $"Error while processing message: {message.Action.Id}");
                message.Action.ActionStatus = ActionStatus.Error;
                message.Action.ActionData.Output.Add("Error message", ex.Message);
                message.Action.ActionData.Output.Add("Stacktrace", ex.StackTrace);
                await uow.ActionRepository.Update(message.Action);
                await uow.SaveChangesAsync();
                throw;
            }
        }

        protected abstract Task<Dictionary<string, string>> ExecuteAsync(DB.Repositories.Action action, CancellationToken cancelToken);
    }
}
