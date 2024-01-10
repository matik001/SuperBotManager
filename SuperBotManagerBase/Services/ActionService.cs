﻿using Microsoft.EntityFrameworkCore;
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
        Task Execute(int executorId, DB.Repositories.Action? fromAction = null);
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
        private Dictionary<string, string> _buildExecuteInput(Dictionary<string, FieldValue> templateInput, ActionSchema? previousAction = null)
        {
            var newInput = templateInput.ToDictionary(a => a.Key, a => a.Value.Value);
            
            if(previousAction != null)
            {
                foreach(var item in previousAction.Output)
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
        public async Task Execute(int executorId, DB.Repositories.Action? fromAction = null)
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
                        Input = _buildExecuteInput(input, fromAction?.ActionData),
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