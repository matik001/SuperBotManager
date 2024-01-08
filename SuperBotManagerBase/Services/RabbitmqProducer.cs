using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.RabbitMq.Core;
using System.Globalization;
using System.Text;

namespace SuperBotManagerBase.Services
{
    public interface IRabbitmqProducer<in TQueueMessage> where TQueueMessage : IQueueMessage
    {
        public void PublishMessage(TQueueMessage obj, string queueName);
    }
    public class RabbitmqProducer<TQueueMessage> : IRabbitmqProducer<TQueueMessage> where TQueueMessage : IQueueMessage
    {
        private readonly IModel _channel;
        private readonly ILogger<RabbitmqProducer<TQueueMessage>> _logger;
        public RabbitmqProducer(IRabbitmqChannelProvider channelProvider, ILogger<RabbitmqProducer<TQueueMessage>> logger)
        {
            _channel = channelProvider.GetChannel();
            _logger = logger;
        }
        public void PublishMessage(TQueueMessage message, string queueName)
        {
            if(Equals(message, default(TQueueMessage))) throw new ArgumentNullException(nameof(message));

            if(message.TimeToLive.Ticks <= 0) throw new QueueingException($"{nameof(message.TimeToLive)} cannot be zero or negative");

            message.MessageId = Guid.NewGuid();

            try
            {
                var serializedMessage = SerializeMessage(message);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.Type = queueName;
                properties.Expiration = message.TimeToLive.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);

                _channel.BasicPublish(queueName, queueName, properties, serializedMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new QueueingException(ex.Message);
            }
        }

        private static byte[] SerializeMessage(TQueueMessage message)
        {
            var stringContent = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(stringContent);
        }
    }
}
