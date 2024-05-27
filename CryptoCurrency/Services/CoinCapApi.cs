using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrency.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoCurrency.Services
{
    public class CoinCapApi
    {
        public static async Task<string> GetApi()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coincap.io/v2/assets?limit=10");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> GetApi(string assetID)
        {
            var client = new HttpClient();
            var rqstr = $"https://api.coincap.io/v2/assets/{assetID}";
            var request = new HttpRequestMessage(HttpMethod.Get, rqstr);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<CryptoList> ReturnList()
        {
            string responseString = await GetApi();
            return JsonConvert.DeserializeObject<CryptoList>(responseString);
        }
        public static async Task<Cryptocurrency> ReturnList(string assetID)
        {
            string responseString = await GetApi(assetID);
            var responseObject = JObject.Parse(responseString);
            var data = responseObject["data"];
            return JsonConvert.DeserializeObject<Cryptocurrency>(data.ToString());
        }
        public static CryptoList GetResultFromTask<CryptoList>(Task<CryptoList> task)
        {
            return task.Result;
        }

        public static async Task<CryptoConvert> GetCryptoRate(string assetID)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.coincap.io/v2/rates/{assetID}");
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("The requested resource was not found (404).");
                }

                response.EnsureSuccessStatusCode();

                var res_str = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(res_str);
                var data = responseObject["data"];
                return JsonConvert.DeserializeObject<CryptoConvert>(data.ToString());
            }
        }
    }
}
