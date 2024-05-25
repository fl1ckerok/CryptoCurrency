using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoCurrency.Models
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
        public static async Task<CryptoList> ReturnList()
        {
            string responseString = await GetApi();
            return JsonConvert.DeserializeObject<CryptoList>(responseString);
        }
        public static CryptoList GetResultFromTask<CryptoList>(Task<CryptoList> task)
        {
            return task.Result;
        }
    }
}
