using Microsoft.EntityFrameworkCore;
using SuperbotConsumerService;
using SuperBotManagerBase.BotDefinitions;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;

var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;
services.AddDbContext<AppDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
services.ConfigureRabbitMq(builder.Configuration);
services.AddQueueMessageConsumer<StorytelSignupActionConsumer.StorytelSignupActionConsumer, ActionQueueMessage>(StorytelActionsProvider.SignUpQueueName);

var logger = services.BuildServiceProvider().GetRequiredService<ILogger<StorytelSignupActionConsumer.StorytelSignupActionConsumer>>();

var host = builder.Build();
host.Run();

public static partial class Program
{
    public static void AddQueueMessageConsumer<TMessageConsumer, TQueueMessage>(this IServiceCollection services, string queueName)
        where TMessageConsumer : IQueueConsumer<TQueueMessage> 
        where TQueueMessage : class, IQueueMessage
    {
        services.AddScoped(typeof(TMessageConsumer));
        services.AddScoped<IRabbitmqQueueProvider<ActionQueueMessage>>(p =>
        {
            var channelProvider = p.GetRequiredService<IRabbitmqChannelProvider>();
            return new RabbitmqQueueProvider<ActionQueueMessage>(channelProvider, queueName);
        });

        services.AddScoped<IQueueConsumerHandler<TMessageConsumer, TQueueMessage>, QueueConsumerHandler<TMessageConsumer, TQueueMessage>>();
        services.AddHostedService<QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage>>();
    }
}