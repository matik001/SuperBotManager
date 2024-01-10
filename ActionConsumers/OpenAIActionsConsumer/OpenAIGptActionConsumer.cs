using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI_API;
using OpenAI_API.Models;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OpenAIActionsConsumer
{
    public class GptActionInput
    {
        public string Key { get; set; }
        public string SystemMessage { get; set; }
        public string Question { get; set; }
        public double Temperature { get; set; }
        public Model Model { get; set; }

        public GptActionInput(Dictionary<string, string> fromInput)
        {
            Question = ConsumersUtils.BuildMessage("Question", fromInput, ["Key", "System message", "Model", "Temperature"]);
            Key = fromInput["Key"];
            SystemMessage = fromInput.ContainsKey("System message") ? fromInput["System message"] : null;
            Temperature = double.Parse(fromInput["Temperature"]);
            Model = fromInput["Model"] switch
            {
                "GPT4" => Model.GPT4_Turbo,
                "GPT3.5" => Model.ChatGPTTurbo,
                _ => Model.GPT4_Turbo,
            };
        }
    }
    [ServiceActionConsumer("openai-gpt-api")]
    public class OpenAIGptActionConsumer : ActionQueueConsumer
    {

        public OpenAIGptActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            GptActionInput input = new GptActionInput(action.ActionData.Input);

            var api = new OpenAIAPI(input.Key);
            var chat = api.Chat.CreateConversation();
            chat.Model = input.Model;
            chat.RequestParameters.Temperature = input.Temperature;
            if(string.IsNullOrEmpty(input.SystemMessage))
                chat.AppendSystemMessage(input.SystemMessage);

            chat.AppendUserInput(input.Question);

            var response = await chat.GetResponseFromChatbotAsync();
            return new Dictionary<string, string> { { "Response", response } };
        }
    }
}
