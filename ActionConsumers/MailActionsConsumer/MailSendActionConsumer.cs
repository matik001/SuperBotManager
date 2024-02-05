using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;

namespace MailActionsConsumer
{
    public class MailSendActionInput
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public bool SSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailSendActionInput(Dictionary<string, string> fromInput)
        {
            Host = fromInput["Host"];
            Port = fromInput["Port"];
            SSL = fromInput["SSL"] == "true";
            Login = fromInput["Login"];
            Password = fromInput["Password"];
            To = ConsumersUtils.BuildMessage("To", fromInput);
            Subject = ConsumersUtils.BuildMessage("Subject", fromInput);
            Body = ConsumersUtils.BuildMessage("Body", fromInput);
        }

    }
    [ServiceActionConsumer("mail-send")]
    public class MailSendActionConsumer : ActionQueueConsumer
    {

        public MailSendActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            MailSendActionInput input = new MailSendActionInput(action.ActionData.Input);

            var res = MailUtils.Send(input);
            return new Dictionary<string, string> { {"Server reponse", res }};
        }
    }
}
