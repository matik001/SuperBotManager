using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;

namespace IntercityActionConsumer
{
    [ServiceActionConsumer("intercity-buy-ticket")]
    public class IntercityBuyTicketActionConsumer : IQueueConsumer<ActionQueueMessage>
    {
        ILogger<IntercityBuyTicketActionConsumer> logger;

        public IntercityBuyTicketActionConsumer(ILogger<IntercityBuyTicketActionConsumer> logger)
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
