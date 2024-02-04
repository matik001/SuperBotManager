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
            //options.Authenticator = new JwtAuthenticator("token");
            options.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
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
            request.AddHeader("X-Api-Key", "jfsTOgOL23CN2G8Y");
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

        /// token will work only 24 hours so You don't need to try it ;D
        public async Task<OpenBoxResponse> OpenBoxWithBearer(int type, string bearer = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkVCRUVGQzgyNzIyOUExMThDQTM1RENGOTM4Q0Y5OTE2QkMxRkM0MjYiLCJ0eXAiOiJKV1QiLCJ4NXQiOiI2LTc4Z25JcG9SaktOZHo1T00tWkZyd2Z4Q1kifQ.eyJuYmYiOjE3MDcwNDc3ODQsImV4cCI6MTcwNzEzNDE4NCwiaXNzIjoiaHR0cHM6Ly9tb2JpbGVhcGkueC1rb20ucGwiLCJhdWQiOlsiaHR0cHM6Ly9tb2JpbGVhcGkueC1rb20ucGwvcmVzb3VyY2VzIiwiYXBpX3YxIl0sImNsaWVudF9pZCI6IkRlZmF1bHRQYXNzd29yZENsaWVudCIsInN1YiI6IjI1OTYzMjciLCJhdXRoX3RpbWUiOjE3MDcwNDc3ODQsImlkcCI6ImxvY2FsIiwidXNlcklkIjoiMjU5NjMyNyIsInVzZXJOYW1lIjoibWF0ZXVzei5raXNpZWwubWtAZ21haWwuY29tIiwic2NvcGUiOlsiYXBpX3YxIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdfQ.V1SWJ9zSD6a7n-T-PEAJK56q1cFddWSqBs4tqDbyUf1FMKZytqDwo8LYqTBeajUVzXBa7RC9_h7Z_VpPhEW67s-iTImILZ5iJ3oeeoYbB2XYwXXX0NPQyzei9QLnQunmFCTMx0jrXIabhxKS-0yrFOuwTX-n3q1TfjoaaEVr3paXheRvGyRL6_gzI2c0OMy-RPs6MEXZjAYz-7YI6qOX3YvtGABq1F7xGacamBsnvvEji7wZIpFnyzNqNd2Yk1mAzctFDeRvlMWWjZ7mY3GkY_5hzcPUVEeYvXHkLHtcAML-T-v_MwMsjcw1bXHPZw2xPX3M2_QzpKwHHHYw7qiUzK9yaQfhABbFIDt13N_kRf1LjPMagDyM-QsM9Ni8iQFrK-_xSFfYgd0B7saOrH6i1PvRKJ8wznK3tG_44yWqBLcefEhWRzPL-HTM9xf-Ofjy9A8BrcAL0-BEEuemdb8NR40gWGD6QGZJ8tMr4iVENv4rpJaVyDqGstSVHDDNU2onIFlXTJ1IW-P1E0kenFcQguggepcR3kTjQ1tkp3Wf3JR5n0xwyy36Mqf8BbywXfT7Khh5_E5qsFc1XL6IG3WnlQzp7AykSCHLst5-hgSIs387c07gfm0U6TMJaun4fcbBo0CGtw5t3V9FlBcXE7Taa_tnqo9m9Bt6QhyIV-UwOqg")
        {
            var request = new RestRequest($"Box/{type}/Roll", Method.Post);
            request.AddHeader("X-Api-Key", $"bekorcfmGwGMw9Nh");
            request.AddHeader("Authorization", $"Bearer {bearer}");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            var response = await client.PostAsync<OpenBoxResponse>(request, CancellationToken.None);
            return response;
        }
    }
}
