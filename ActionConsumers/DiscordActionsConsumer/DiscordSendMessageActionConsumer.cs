using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace DiscordActionsConsumer
{
    public class SendMessageInput
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool TagEveryone { get; set; }

        public SendMessageInput(Dictionary<string, string> fromInput) 
        { 
            Message = ConsumersUtils.BuildMessage("Message", fromInput, ["Tag everyone", "Token"]);
            TagEveryone = fromInput["Tag everyone"] == "true";
            Token = fromInput["Token"];
        }
    }
    [ServiceActionConsumer("discord-send-message")]
    public class DiscordSendMessageActionConsumer : ActionQueueConsumer
    {
        public DiscordSendMessageActionConsumer(ILogger<DiscordSendMessageActionConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }

        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new SendMessageInput(action.ActionData.Input);
            if(input.TagEveryone)
                input.Message = $"@everyone {input.Message}";
            await new DiscordBot(input.Token).SendMessage(input.Message);
            return new Dictionary<string, string>();
        }
    }
}
