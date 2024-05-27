using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.Models
{
    public class CryptoDataChart
    {
        [JsonProperty("priceUsd")]
        public string PriceUsd { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
