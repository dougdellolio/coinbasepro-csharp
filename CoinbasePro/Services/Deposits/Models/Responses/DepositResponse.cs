using System;
using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Deposits.Models.Responses
{
    public class DepositResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency Currency { get; set; }

        public DateTime PayoutAt { get; set; }
    }
}
