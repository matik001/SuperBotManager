using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;

namespace StorytelActionsConsumer
{
    [ServiceActionConsumer("storytel-sign-up")]
    public class StorytelSignupActionConsumer : IQueueConsumer<ActionQueueMessage>
    {
        ILogger<StorytelSignupActionConsumer> logger;

        public StorytelSignupActionConsumer(ILogger<StorytelSignupActionConsumer> logger)
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
