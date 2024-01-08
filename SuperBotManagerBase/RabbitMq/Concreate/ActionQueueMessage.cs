using SuperBotManagerBase.RabbitMq.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Concreate
{
    public class ActionQueueMessage : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public DB.Repositories.Action Action { get; set; }
    }
}
