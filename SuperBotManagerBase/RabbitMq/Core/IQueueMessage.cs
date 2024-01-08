using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Core
{
    public interface IQueueMessage
    {
        Guid MessageId { get; set; }
        TimeSpan TimeToLive { get; set; }
    }
}
