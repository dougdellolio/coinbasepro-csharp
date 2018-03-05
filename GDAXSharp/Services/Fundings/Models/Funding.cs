using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Fundings.Models
{
    public class Funding
    {
        public Guid Id { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FundingStatus Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        [JsonProperty("repaid_amount")]
        public decimal RepaidAmount { get; set; }

        [JsonProperty("default_amount")]
        public decimal DefaultAmount { get; set; }

        [JsonProperty("repaid_default")]
        public bool RepaidDefault { get; set; }
    }
}
