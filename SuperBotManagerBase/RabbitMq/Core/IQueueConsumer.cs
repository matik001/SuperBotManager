using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.RabbitMq.Core
{
    public interface IQueueConsumer<in TQueueMessage> where TQueueMessage : class, IQueueMessage
    {
        Task ConsumeAsync(TQueueMessage message);
    }
}
