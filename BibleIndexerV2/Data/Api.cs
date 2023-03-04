using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BibleIndexerV2.Data
{
    internal static class Api
    {

        private static RestClient? _client;

        /// <Summary></Summary>:
        private static RestClient GetClient()
        {
            var url = (new ConfigurationBuilder().AddUserSecrets<BibleService>()).Build().GetSection("api_url").Value;
            // builder.GetSection("api_url").Get<string>();

            _client = null ?? new RestClient(url);
            return _client;
            /*_client = null ?? new RestClient("https://gist.githubusercontent.com/king-Alex-d-great/b32f98847970708f4fbba9c94cd9a3a1/raw/97459a7dc59eaeff42c7f5d22cf1553208430e9f/");
            return _client;*/
        }


        /// <Summary>Get bible books, chapters and verses in JSON format</Summary>:
        public static async Task<List<dynamic>> GetBibleBlob()
        {
            RestClient client = GetClient();
            RestRequest request = new RestRequest("kjv.json");

            RestResponse response = await client.ExecuteGetAsync(request);
            if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error: API call failed\nTip: Check that you are connected to the internet");
            }

            return JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
        }
    }
}
