using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;

namespace DiscordActionsConsumer
{
    public class SendMessageInput
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool TagEveryone { get; set; }

        public SendMessageInput(Dictionary<string, string> fromInput) 
        { 
            Message = fromInput["Message"];
            TagEveryone = fromInput["Tag everyone"] == "true";
            Token = fromInput["Token"];
        }
    }
    [ServiceActionConsumer("discord-send-message")]
    public class DiscordSendMessageActionConsumer : IQueueConsumer<ActionQueueMessage>
    {
        ILogger<DiscordSendMessageActionConsumer> logger;

        public DiscordSendMessageActionConsumer(ILogger<DiscordSendMessageActionConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task ConsumeAsync(ActionQueueMessage message)
        {
            logger.LogInformation($"Executing: {message.Action.Id} ({message.Action.ActionExecutor.ActionExecutorName} - {message.Action.ActionExecutor.ActionDefinition.ActionDefinitionName})");
            await Task.CompletedTask;
            var input = new SendMessageInput(message.Action.ActionData.Input);
            if(input.TagEveryone)
                input.Message = $"@everyone {input.Message}";
            await new DiscordBot(input.Token).SendMessage(input.Message);
        }
    }
}
