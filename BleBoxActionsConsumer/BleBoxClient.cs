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
            var client = new RestClient(ip);

            var request = new RestRequest("/s/offon/last", Method.Get);

            var response = client.Execute(request);
            return JObject.Parse(response.Content);
        }

    }
}
