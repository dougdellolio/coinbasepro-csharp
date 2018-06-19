using System;
using CoinbasePro.Services.Accounts.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Accounts.Models
{
    public class AccountHold
    {
        public string Id { get; set; }

        public Guid AccountId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountHoldType AccountHoldType { get; set; }

        public Guid Ref { get; set; }
    }
}
