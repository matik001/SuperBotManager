using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace MailActionsConsumer
{
    public class ReadMailActionInput
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public bool SSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string MailBox { get; set; }
        public string SearchPhrase { get; set; }
        public ReadMailActionInput(Dictionary<string, string> fromInput)
        {
            Host = fromInput["Host"];
            Port = fromInput["Port"];
            SSL = fromInput["SSL"] == "true";
            Login = fromInput["Login"];
            Password = fromInput["Password"];
            MailBox = ConsumersUtils.BuildMessage("MailBox", fromInput);
            SearchPhrase = ConsumersUtils.BuildMessage("SearchPhrase", fromInput);
        }
    }
    [ServiceActionConsumer("mail-read")]
    public class MailReadActionConsumer : ActionQueueConsumer
    {

        public MailReadActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            ReadMailActionInput input = new ReadMailActionInput(action.ActionData.Input);


            return new Dictionary<string, string> { { "Response", "response" } };
        }
    }
}
