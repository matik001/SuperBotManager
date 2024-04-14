using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;

namespace BleBoxActionsConsumer
{
    public class LightChangeActionInput
    {
        public string BleBoxIP { get; set; }
        public string Color { get; set; }
        public int? FadeMs { get; set; }
        public int? ForTime { get; set; }

        public LightChangeActionInput(Dictionary<string, string> fromInput)
        {
            BleBoxIP = fromInput["BleBoxIP"];
            Color = fromInput["Color"];
            if(fromInput.ContainsKey("FadeMs") && !string.IsNullOrEmpty(fromInput["FadeMs"]) )
                FadeMs = int.Parse(fromInput["FadeMs"]);
            if(fromInput.ContainsKey("ForTime") && !string.IsNullOrEmpty(fromInput["ForTime"]))
                ForTime = int.Parse(fromInput["ForTime"]);
        }
    }
    [ServiceActionConsumer("light-change")]
    public class BleBoxLightChangeActionConsumer : ActionQueueConsumer
    {
        public BleBoxLightChangeActionConsumer(ILogger<BleBoxLightSwitchActionConsumer> logger, IAppUnitOfWork uow, IActionService actionService) : base(logger, uow, actionService)
        {
        }

        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action)
        {
            var input = new LightChangeActionInput(action.ActionData.Input);
            BleBoxClient client = new BleBoxClient();
            var res = client.ChangeLight(input);
            return new Dictionary<string, string> { { "Response", res.ToString() } };
        }
    }
}
