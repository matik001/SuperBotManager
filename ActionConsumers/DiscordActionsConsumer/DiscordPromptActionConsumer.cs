using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace DiscordActionsConsumer
{
    public class PromptActionInput
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool TagEveryone { get; set; }

        public PromptActionInput(Dictionary<string, string> fromInput) 
        {
            Message = ConsumersUtils.BuildMessage("Message", fromInput, ["Tag everyone", "Token "]);
            TagEveryone = fromInput["Tag everyone"] == "true";
            Token = fromInput["Token"];
        }
    }
    [ServiceActionConsumer("discord-prompt")]
    public class DiscordPromptActionConsumer : ActionQueueConsumer
    {
        public DiscordPromptActionConsumer(ILogger<DiscordPromptActionConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }

        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new SendMessageInput(action.ActionData.Input);
            if(input.TagEveryone)
                input.Message = $"@everyone {input.Message}";
            var answer = await new DiscordBot(input.Token).Prompt(input.Message);
            return new Dictionary<string, string> { { "Answer", answer } };
        }
    }
}
