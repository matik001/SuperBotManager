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
            producer.PublishMessage(msg, action.ActionExecutor.ActionDefinition.ActionDefinitionQueueName);
        }
    }
}
