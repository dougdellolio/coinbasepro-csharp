using Newtonsoft.Json;
using System;
using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Accounts.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public decimal Hold { get; set; }

        public decimal Available { get; set; }
    }
}
