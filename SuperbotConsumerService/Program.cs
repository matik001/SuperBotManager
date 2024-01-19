using Microsoft.EntityFrameworkCore;
using SuperbotConsumerService;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;
using System.Reflection;

var loadedConsumerAssemblies = AssemblyUtils.LoadAssembliesContainingName("Consumer");
var consumers = loadedConsumerAssemblies.SelectMany(a => ServiceUtils.GetConsumersForAssembly(a)).ToList();

var loadedServicesAssemblies = AssemblyUtils.LoadAssembliesContainingName("BackgroundService");
var backgroundServices = loadedServicesAssemblies.SelectMany(a => ServiceUtils.GetBackgroundServicesForAssembly(a)).ToList();


var jobsServices = consumers.Cast<IServiceInfo>().Union(backgroundServices).ToList();

List<IHost> hosts = new List<IHost>();
foreach(var job in jobsServices)
{
    var builder = Host.CreateApplicationBuilder(args);
    var services = builder.Services;
    services.AddDbContext<AppDBContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if(System.Diagnostics.Debugger.IsAttached)
        {
            options.UseMySQL(connectionString, b => b
                .MigrationsAssembly("SuperBotManagerBackend"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else
        {
            options.UseMySQL(connectionString, b => b
                .MigrationsAssembly("SuperBotManagerBackend"));
        };
    });
    services.AddSingleton<IConfigurationManager>(_=>builder.Configuration);
    services.ConfigureEncryption(builder.Configuration);
    services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
    services.ConfigureRabbitMq(builder.Configuration);
    services.AddScoped<IActionService, ActionService>();
    services.AddScoped<ISeleniumProvider, SeleniumProvider>();
    if(job is BackgroundServiceInfo bgService)
    {
        var serviceCollectionExtensionsType = typeof(ServiceCollectionHostedServiceExtensions);
        var addHostedServiceMethod = serviceCollectionExtensionsType.GetMethod("AddHostedService", new[] { typeof(IServiceCollection) });
        addHostedServiceMethod.MakeGenericMethod(bgService.ServiceType).Invoke(null, new object[] { services });

    }
    else if(job is ConsumerServiceInfo consumer)
    {
        services.AddQueueMessageConsumer(consumer.ConsumerType, typeof(ActionQueueMessage), consumer.QueueName);
    }

    var host = builder.Build();
    hosts.Add(host);
}

Task.WaitAll(hosts.Select(h => h.RunAsync()).ToArray());


