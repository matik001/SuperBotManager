using Microsoft.EntityFrameworkCore;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.RabbitMq.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Services
{
    public interface IActionService
    {
        Task Execute(int executorId);
    }
    public class ActionService : IActionService
    {
        private readonly IAppUnitOfWork uow;
        private readonly IActionProducer actionProducer;

        public ActionService(IAppUnitOfWork uow, IActionProducer actionProducer)
        {
            this.uow = uow;
            this.actionProducer = actionProducer;
        }

        public async Task Execute(int executorId)
        {
            var executor = await uow.ActionExecutorRepository.GetById(executorId, a => a.Include(x => x.ActionDefinition));
            if(!executor.IsValid)
            {
                throw new Exception("Executor is not valid, so cannot be run");
            }

            var executorData = executor.ActionData.DeepClone();
            await executorData.DecryptSecrets(executor.ActionDefinition.ActionDataSchema, uow);

            var newActions = executorData.Inputs.Select(input =>
            {
                var action = new DB.Repositories.Action()
                {
                    ActionExecutorId = executor.Id,
                    ActionStatus = ActionStatus.Pending,
                    ActionData = new ActionSchema()
                    {
                        Input = input.ToDictionary(a => a.Key, a => a.Value.Value),
                        Output = new Dictionary<string, string>()
                    },
                };

                return action;
            }).ToList();
            foreach(var action in newActions)
            {
                await uow.ActionRepository.Create(action);
            }
            executor.LastRunDate = DateTime.UtcNow;
            await uow.ActionExecutorRepository.Update(executor);
            await uow.SaveChangesAsync(); /// in case consumer will be faster

            foreach(var action in newActions)
            {
                action.ActionExecutor = executor;
                actionProducer.SendToExecute(action);
            }
            if(!executor.PreserveExecutedInputs) 
            {
                executor.ActionData.Inputs.Clear();
                await uow.ActionExecutorRepository.Update(executor);
                await uow.SaveChangesAsync(); 
            }
        }
    }
}
