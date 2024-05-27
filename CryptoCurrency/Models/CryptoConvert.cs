using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrency.Models
{
    public class CryptoConvert
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("rateUsd")]
        public string Rate { get; set; }
    }
}
