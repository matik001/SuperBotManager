using Microsoft.EntityFrameworkCore;
using SuperbotConsumerService;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.BotDefinitions;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using System.Reflection;


ServiceUtils.LoadConsumersAssemblies();
var consumers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => ServiceUtils.GetConsumersForAssembly(a)).ToList();
List<IHost> hosts = new List<IHost>();
foreach(var consumer in consumers)
{
    var builder = Host.CreateApplicationBuilder(args);
    var services = builder.Services;
    services.AddDbContext<AppDBContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(connectionString);
    });
    services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
    services.ConfigureRabbitMq(builder.Configuration);

    services.AddQueueMessageConsumer(consumer.ConsumerType, typeof(ActionQueueMessage), consumer.QueueName);

    var host = builder.Build();
    hosts.Add(host);
}

Task.WaitAll(hosts.Select(h => h.RunAsync()).ToArray());


