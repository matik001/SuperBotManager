using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BleBoxActionsConsumer
{
    public class BleBoxClient
    {
        public BleBoxClient()
        {
        }
        public JObject SwitchLight(string ip)
        {
            var client = new RestClient($"http://{ip}");

            var request = new RestRequest("/s/offon/last", Method.Get);

            var response = client.Execute(request);
            return JObject.Parse(response.Content);
        }

        public JObject ChangeLight(LightChangeActionInput changeInfo)
        {
            var client = new RestClient($"http://{changeInfo.BleBoxIP}");

            string url = $"/s/{changeInfo.Color}";
            if(changeInfo.FadeMs.HasValue)
                url += $"/colorFadeMs/{changeInfo.FadeMs}";
            if(changeInfo.ForTime.HasValue)
                url += $"/forTime/{changeInfo.ForTime}";
            var request = new RestRequest($"{url}", Method.Get);

            var response = client.Execute(request);
            return JObject.Parse(response.Content);
        }
    }
}
