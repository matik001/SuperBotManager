using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Attributes
{
    public class ServiceActionConsumerAttribute : Attribute
    {
        public ServiceActionConsumerAttribute(string queueName) { 
            this.QueueName = queueName; 
        }

        public string QueueName { get; }
    }
}
