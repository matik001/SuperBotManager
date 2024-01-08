using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;
using System.Runtime.Serialization;
using System.Text;
namespace SuperBotManagerBase.Configuration
{
    public static class RabbitMqConfig
    {
        public static string Hostname { get; private set; }
        public static string Username { get; private set; }
        public static string Password { get; private set; }
        public static string VirtualHost { get; private set; }
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfigurationManager configuration)
        {
            var section = configuration.GetSection("RabbitMQ");
            if(section == null)
            {
                throw new Exception("RabbitMQ section not found in appsettings.json");
            }
            Hostname = section.GetValue<string>("Hostname");
            Username = section.GetValue<string>("Username");
            Password = section.GetValue<string>("Password");
            VirtualHost = section.GetValue<string>("VirtualHost");

            services.AddSingleton<IAsyncConnectionFactory>(provider =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = RabbitMqConfig.Hostname,
                    UserName = RabbitMqConfig.Username,
                    Password = RabbitMqConfig.Password,
                    VirtualHost = RabbitMqConfig.VirtualHost,

                    DispatchConsumersAsync = true,
                    AutomaticRecoveryEnabled = true,

                    //ConsumerDispatchConcurrency = settings.RabbitMqConsumerConcurrency.GetValueOrDefault(),
                };

                return factory;
            });
            services.AddScoped<IRabbitmqConnectionProvider, RabbitmqConnectionProvider>();
            services.AddScoped<IRabbitmqChannelProvider, RabbitmqChannelProvider>();
            services.AddScoped(typeof(IRabbitmqProducer<>), typeof(RabbitmqProducer<>));
            services.AddScoped<IActionProducer, ActionProducer>();
        }
    }
    public class QueueingException : Exception
    {
        public QueueingException()
        {
        }

        public QueueingException(string? message) : base(message)
        {
        }

        public QueueingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected QueueingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
