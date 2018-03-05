using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Withdrawals.Models
{
    public class Coinbase
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        [JsonProperty("coinbase_account_id")]
        public Guid CoinbaseAccountId { get; set; }
    }
}
