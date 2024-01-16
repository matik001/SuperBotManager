using SuperBotManagerBase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Concreate
{
    public interface IActionProducer
    {
        void SendToExecute(DB.Repositories.Action action);
    }

    public class ActionProducer : IActionProducer
    {
        IRabbitmqProducer<ActionQueueMessage> producer;
        public ActionProducer(IRabbitmqProducer<ActionQueueMessage> producer)
        {
            this.producer = producer;
        }
        public void SendToExecute(DB.Repositories.Action action) 
        {
            var msg = new ActionQueueMessage()
            {
                TimeToLive = TimeSpan.FromDays(10),
                Action = action
            };
            /// to prevent circle references when serializing to json
            var prevExecutors = action.ActionExecutor.ActionDefinition.ActionExecutors;
            var prevActions = action.ActionExecutor.Actions;
            action.ActionExecutor.ActionDefinition.ActionExecutors = null;

            action.ActionExecutor.Actions = null;
            action.ForwardedFromAction = null;
            producer.PublishMessage(msg, action.ActionExecutor.ActionDefinition.ActionDefinitionQueueName);
            action.ActionExecutor.ActionDefinition.ActionExecutors = prevExecutors; ;
            action.ActionExecutor.Actions = prevActions; ;
        }
    }
}
