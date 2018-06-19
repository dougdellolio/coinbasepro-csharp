using System;
using CoinbasePro.Services.Fundings.Types;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Fundings.Models
{
    public class Funding
    {
        public Guid Id { get; set; }

        public string OrderId { get; set; }

        public string ProfileId { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FundingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public decimal RepaidAmount { get; set; }

        public decimal DefaultAmount { get; set; }

        public bool RepaidDefault { get; set; }
    }
}
