using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using SuperBotManagerBackend.Configuration;
using System.Text;

namespace SuperBotManagerBackend.Services
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T obj);
    }
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T obj)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitMqConfig.Hostname,
                UserName = RabbitMqConfig.Username,
                Password = RabbitMqConfig.Password,
                VirtualHost = RabbitMqConfig.VirtualHost
            };
            using var connection = factory.CreateConnection();
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
