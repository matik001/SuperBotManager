using RabbitMQ.Client;
using SuperBotManagerBase.RabbitMq.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Services
{
    public interface IRabbitmqQueueProvider<TQueueMessage> where TQueueMessage : IQueueMessage
    {
        public IModel GetChannel();
        public string QueueName {get;}
    }

    public class RabbitmqQueueProvider<TQueueMessage> : IRabbitmqQueueProvider<TQueueMessage> where TQueueMessage : IQueueMessage
    {
        private readonly IRabbitmqChannelProvider _channelProvider;
        private IModel _channel;
        private readonly string _queueName;

        public RabbitmqQueueProvider(IRabbitmqChannelProvider channelProvider, string queueName)
        {
            _channelProvider = channelProvider;
            _queueName = queueName;
        }
        public string QueueName { get => _queueName;}

        public IModel GetChannel()
        {
            _channel = _channelProvider.GetChannel();
            DeclareQueueAndDeadLetter();
            return _channel;
        }

        private void DeclareQueueAndDeadLetter()
        {
            /// Error queue
            var deadLetterQueueName = $"{_queueName}-errors";
            var deadLetterQueueArgs = new Dictionary<string, object>
            {
                { "x-queue-type", "quorum" },
                { "overflow", "reject-publish" } // If the queue is full, reject the publish
            };

            _channel.ExchangeDeclare(deadLetterQueueName, ExchangeType.Direct);
            _channel.QueueDeclare(deadLetterQueueName, true, false, false, deadLetterQueueArgs);
            _channel.QueueBind(deadLetterQueueName, deadLetterQueueName, deadLetterQueueName, null);


            /// Main queue
            var queueArgs = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", deadLetterQueueName },
                { "x-dead-letter-routing-key", deadLetterQueueName },
                { "x-queue-type", "quorum" },
                { "x-dead-letter-strategy", "at-least-once" }, // Ensure that deadletter messages are delivered in any case see: https://www.rabbitmq.com/quorum-queues.html#dead-lettering
                { "overflow", "reject-publish" } // If the queue is full, reject the publish
            };

            _channel.ExchangeDeclare(_queueName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueName, true, false, false, queueArgs);
            _channel.QueueBind(_queueName, _queueName, _queueName, null);
        }
    }
}
