using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using System;

namespace CoinbasePro.Services.Profiles.Models
{
    public class ProfileTransfer
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency Currency { get; set; }

        public decimal Amount { get; set; }
    }
}
