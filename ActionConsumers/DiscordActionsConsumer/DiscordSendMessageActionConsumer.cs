using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;

namespace DiscordActionsConsumer
{
    [ServiceActionConsumer("discord-send-message")]
    public class DiscordSendMessageActionConsumer : IQueueConsumer<ActionQueueMessage>
    {
        ILogger<DiscordSendMessageActionConsumer> logger;

        public DiscordSendMessageActionConsumer(ILogger<DiscordSendMessageActionConsumer> logger)
        {
            this.logger = logger;
        }

        public Task ConsumeAsync(ActionQueueMessage message)
        {
            logger.LogInformation($"Executing: {message.Action.Id} ({message.Action.ActionExecutor.ActionExecutorName} - {message.Action.ActionExecutor.ActionDefinition.ActionDefinitionName})");
            return Task.CompletedTask;
        }
    }
}
