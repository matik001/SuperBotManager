using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace StorytelActionsConsumer
{
    public class StorytelSignupInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CardNumber { get; set; }
        public string CardCCV { get; set; }
        public DateTime CardExpiration { get; set; }

        public StorytelSignupInput(Dictionary<string, string> fromInput)
        {
            Email = fromInput["Email"];
            Password = fromInput["Password"];
            CardNumber = fromInput["Card number"];
            CardCCV = fromInput["Card CCV"];
            CardExpiration = DateTime.Parse(fromInput["Card expiration"]);
        }
    }
    [ServiceActionConsumer("storytel-sign-up")]
    public class StorytelSignupActionConsumer : ActionQueueConsumer
    {

        public StorytelSignupActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new StorytelSignupInput(action.ActionData.Input);
            new StorytelBot().CreateAccount(input);
            return new Dictionary<string, string> {  };
        }
    }
}
