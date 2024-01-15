﻿using Microsoft.EntityFrameworkCore;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.RabbitMq.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SuperBotManagerBase.Services
{
    public interface IActionService
    {
        Task EnqueueExecution(int executorId, DB.Repositories.Action? fromAction = null);
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
        private async Task<Dictionary<string, string>> _buildExecuteInput(Dictionary<string, FieldValue> templateInput, ActionSchema? previousAction = null)
        {
            var newInput = templateInput.ToDictionary(a => a.Key, a => a.Value.Value);
            if(previousAction != null)
            {
                var output = await ActionSchema.DecryptDict(uow, previousAction.Output);
                foreach(var item in output)
                {
                    newInput[item.Key] = item.Value;
                }

                /// pass forward inputs from previous action, without overriding 
                foreach(var item in previousAction.Input)
                {
                    if(!newInput.ContainsKey(item.Key))
                        newInput[item.Key] = item.Value;
                }
            }
            return newInput;
        }
/// <summary>
/// formAction is encrypted
/// </summary>
        public async Task EnqueueExecution(int executorId, DB.Repositories.Action? fromAction = null)
        {
            var executor = await uow.ActionExecutorRepository.GetById(executorId, a => a.Include(x => x.ActionDefinition));
            if(!executor.IsValid)
            {
                throw new Exception("Executor is not valid, so cannot be run");
            }

            var executorData = executor.ActionData.DeepClone();
            await executorData.DecryptSecrets(executor.ActionDefinition.ActionDataSchema, uow);

            var newActions = await Task.WhenAll(executorData.Inputs.Select(async input =>
            {
                var action = new DB.Repositories.Action()
                {
                    ActionExecutorId = executor.Id,
                    ActionStatus = ActionStatus.Pending,
                    ActionData = new ActionSchema()
                    {
                        Input = await _buildExecuteInput(input, fromAction?.ActionData),
                        Output = new Dictionary<string, string>()
                    },
                };
                action.ActionData.Input = await ActionSchema.EncryptDict(uow, action.ActionData.Input);

                return action;
            }));
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
