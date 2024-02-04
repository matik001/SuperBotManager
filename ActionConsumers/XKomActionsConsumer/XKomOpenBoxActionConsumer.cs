using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using RestSharp;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using System.Threading;

namespace XKomActionsConsumer
{
    public class XKomOpenBoxInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Box { get; set; }
        public XKomOpenBoxInput(Dictionary<string, string> fromInput)
        {
            Email = fromInput["Email"];
            Password = fromInput["Password"];
            Box = fromInput["Box"];
        }
    }

    [ServiceActionConsumer("xkom-open-boxes")]
    public class XKomOpenBoxActionConsumer : ActionQueueConsumer
    {
        public XKomOpenBoxActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }

        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new XKomOpenBoxInput(action.ActionData.Input);
            var client = new XKomClient();
            //await client.Login(input.Email, input.Password);
            var res = await client.OpenBoxWithBearer(int.Parse(input.Box));
            return new Dictionary<string, string>
            {
                { "ItemName", res.Item.Name },
                { "CatalogPrice", res.Item.CatalogPrice.ToString() },
                { "PromotionGainValue", res.PromotionGain.Value.ToString() }
            };
        }
    }
}
