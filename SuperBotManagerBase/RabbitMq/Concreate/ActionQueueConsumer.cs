using Microsoft.Extensions.Logging;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task ConsumeAsync(ActionQueueMessage message)
        {
            var action = message.Action;

            logger.LogInformation($"Processing: {action.Id} ({action.ActionExecutor.ActionExecutorName} - {action.ActionExecutor.ActionDefinition.ActionDefinitionName})");
            action.ActionStatus = ActionStatus.InProgress;
            await uow.ActionRepository.Update(action);
            await uow.SaveChangesAsync();

            action.ActionData = await ActionSchema.Decrypt(uow, action.ActionData, action.ActionExecutor.ActionDefinition.ActionDataSchema);

            var output = await ExecuteAsync(action);

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

        protected abstract Task<Dictionary<string, string>> ExecuteAsync(DB.Repositories.Action action);
    }
}
