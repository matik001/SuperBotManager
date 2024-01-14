using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperbotConsumerService
{
    interface IServiceInfo
    {
    }
    internal class ConsumerServiceInfo : IServiceInfo
    {
        public Type ConsumerType { get; set; }
        public string QueueName { get; set; }
    }
    internal class BackgroundServiceInfo : IServiceInfo
    {
        public Type ServiceType { get; set; }
    }
    internal static class ServiceUtils
    {
        internal static IEnumerable<ConsumerServiceInfo> GetConsumersForAssembly(Assembly assembly)
        {
            var consumers = assembly.GetClassesWithAttribute<ServiceActionConsumerAttribute>();
            var consumerInfos = consumers.Select(consumerType =>
            {
                var attribute = consumerType.GetCustomAttribute<ServiceActionConsumerAttribute>();
                var consumerInfo = new ConsumerServiceInfo
                {
                    ConsumerType = consumerType,
                    QueueName = attribute.QueueName
                };
                return consumerInfo;
            });

            return consumerInfos;
        }
        internal static IEnumerable<BackgroundServiceInfo> GetBackgroundServicesForAssembly(Assembly assembly)
        {
            var classes = assembly.GetClassesWithAttribute<BackgroundServiceAttribute>();
            var servicesInfos = classes.Select(consumerType =>
            {
                var serviceInfo = new BackgroundServiceInfo
                {
                    ServiceType = consumerType,
                };
                return serviceInfo;
            });

            return servicesInfos;
        }
        //where TMessageConsumer : IQueueConsumer<TQueueMessage> 
        //where TQueueMessage : class, IQueueMessage
        internal static void AddQueueMessageConsumer(this IServiceCollection services, Type TMessageConsumer, Type TQueueMessage, string queueName)
        {
            services.AddScoped(TMessageConsumer);
            services.AddScoped<IRabbitmqQueueProvider<ActionQueueMessage>>(p =>
            {
                var channelProvider = p.GetRequiredService<IRabbitmqChannelProvider>();
                return new RabbitmqQueueProvider<ActionQueueMessage>(channelProvider, queueName);
            });

            var handlerIType = typeof(IQueueConsumerHandler<,>).MakeGenericType(TMessageConsumer, TQueueMessage);
            var handlerType = typeof(QueueConsumerHandler<,>).MakeGenericType(TMessageConsumer, TQueueMessage);

            services.AddScoped(handlerIType, handlerType);

            var consumerQueueService = typeof(QueueConsumerRegistratorService<,>).MakeGenericType(TMessageConsumer, TQueueMessage);

            var serviceCollectionExtensionsType = typeof(ServiceCollectionHostedServiceExtensions);
            var addHostedServiceMethod = serviceCollectionExtensionsType.GetMethod("AddHostedService", new[] { typeof(IServiceCollection) });

            //var addHostedService = services.GetType().GetMethod("AddHostedService");
            addHostedServiceMethod.MakeGenericMethod(consumerQueueService).Invoke(null, new object[] { services });
        }


    }
}
