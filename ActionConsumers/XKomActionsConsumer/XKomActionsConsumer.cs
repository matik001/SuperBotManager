using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;

namespace XKomActionsConsumer
{
    [ServiceActionConsumer("xkom-open-boxes")]
    public class XKomActionsConsumer : IQueueConsumer<ActionQueueMessage>
    {
        ILogger<XKomActionsConsumer> logger;

        public XKomActionsConsumer(ILogger<XKomActionsConsumer> logger)
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
