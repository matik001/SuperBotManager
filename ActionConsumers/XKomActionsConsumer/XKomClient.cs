using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XKomActionsConsumer
{
    public class XKomClient
    {
        RestClient client;
        LoginResponse authData;
        static readonly string BaseURL = "https://mobileapi.x-kom.pl/api/v1/xkom";

        public XKomClient()
        {
            var options = new RestClientOptions(BaseURL);
            options.Authenticator = new JwtAuthenticator("token");
            client = new RestClient(options);
        }

        public class LoginResponse
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
        }
        public async Task Login(string email, string password)
        {
            var request = new RestRequest("Token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("X-Api-Key", "bekorcfmGwGMw9Nh");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", email);
            request.AddParameter("password", password);
            var response = await client.ExecuteAsync<LoginResponse>(request, CancellationToken.None);
            this.authData = response.Data;

            var options = new RestClientOptions(BaseURL);
            options.Authenticator = new JwtAuthenticator(authData.access_token);
            client = new RestClient(options);
        }


        public class PromotionGain
        {
            public double Value { get; set; }
            public string GainValue { get; set; }
            public string GainType { get; set; }
        }
        public class Item
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public double CatalogPrice { get; set; }
        }
        public class OpenBoxResponse
        {
            public string BoxItemRolledResourceId { get; set; }
            public double BoxPrice { get; set; }
            public string WebUrl { get; set; }
            public PromotionGain PromotionGain { get; set; }
            public Item Item { get; set; } 

        }
        public async Task<OpenBoxResponse> OpenBox(int type)
        {
            var request = new RestRequest($"Box/{type}/Roll", Method.Post);
            var response = await client.PostAsync< OpenBoxResponse>(request, CancellationToken.None);
            return response;
        }
    }
}
