using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Accounts.Models
{
    public class AccountHold
    {
        public string Id { get; set; }

        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountHoldType AccountHoldType { get; set; }

        public Guid Ref { get; set; }
    }
}
