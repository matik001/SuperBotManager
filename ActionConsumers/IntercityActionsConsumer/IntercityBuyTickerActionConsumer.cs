using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace IntercityActionConsumer
{
    public enum ICDiscount
    {
        None, Student
    }
    public class ICBuyTicketActionInput
    {
        public DateTime TripDate { get; set; }
        public string TicketOwner { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICDiscount Discount { get; set; }

        public ICBuyTicketActionInput(Dictionary<string, string> fromInput)
        {
            //Question = ConsumersUtils.BuildMessage("Question", fromInput);
            //Key = fromInput["Key"];
            //SystemMessage = fromInput.ContainsKey("System message") ? fromInput["System message"] : null;
            //Temperature = double.Parse(fromInput["Temperature"]);
            //Model = fromInput["Model"] switch
            //{
            //    "GPT4" => Model.GPT4_Turbo,
            //    "GPT3.5" => Model.ChatGPTTurbo,
            //    _ => Model.GPT4_Turbo,
            //};
        }
    }
    [ServiceActionConsumer("intercity-buy-ticket")]
    public class IntercityBuyTicketActionConsumer : ActionQueueConsumer
    {

        public IntercityBuyTicketActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            //GptActionInput input = new GptActionInput(action.ActionData.Input);

            //var api = new OpenAIAPI(input.Key);
            //var chat = api.Chat.CreateConversation();
            //chat.Model = input.Model;
            //chat.RequestParameters.Temperature = input.Temperature;
            //if(string.IsNullOrEmpty(input.SystemMessage))
            //    chat.AppendSystemMessage(input.SystemMessage);

            //chat.AppendUserInput(input.Question);

            //var response = await chat.GetResponseFromChatbotAsync();
            return new Dictionary<string, string> {  };
        }
    }

}
