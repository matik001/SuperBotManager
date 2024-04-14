using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;

namespace BleBoxActionsConsumer
{
    public class LightSwitchActionInput
    {
        public string BleBoxIP { get; set; }

        public LightSwitchActionInput(Dictionary<string, string> fromInput)
        {
            BleBoxIP = fromInput["BleBoxIP"];
        }
    }
    [ServiceActionConsumer("light-switch")]
    public class BleBoxLightSwitchActionConsumer : ActionQueueConsumer
    {
        public BleBoxLightSwitchActionConsumer(ILogger<BleBoxLightSwitchActionConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }

        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new LightSwitchActionInput(action.ActionData.Input);
            BleBoxClient client = new BleBoxClient();
            var res = client.SwitchLight(input.BleBoxIP);
            return new Dictionary<string, string> { { "Response", res.ToString() } };
        }
    }
}
