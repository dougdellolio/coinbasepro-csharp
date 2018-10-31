using System;
using CoinbasePro.Services.Accounts.Types;
using CoinbasePro.Shared.JsonConverters;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Accounts.Models
{
    public class AccountHistory
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountEntryType AccountEntryType { get; set; }

        public Details Details { get; set; }
    }

    public class Details
    {
        public Guid OrderId { get; set; }

        public string TradeId { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public ProductType ProductId { get; set; }
    }
}
