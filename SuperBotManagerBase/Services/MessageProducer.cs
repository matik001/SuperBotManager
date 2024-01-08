using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using SuperBotManagerBase.Configuration;
using System.Text;

namespace SuperBotManagerBase.Services
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T obj);
    }
    public class MessageProducer : IMessageProducer
    {
        private readonly IAsyncConnectionFactory _connectionFactory;
        public MessageProducer(IAsyncConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void SendMessage<T>(T obj)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: typeof(T).Name,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var json =  JObject.FromObject(obj);
            var bytes = Encoding.UTF8.GetBytes(json.ToString());

            channel.BasicPublish(exchange: "", routingKey: typeof(T).Name, basicProperties: null, body: bytes);
        }
    }
}
