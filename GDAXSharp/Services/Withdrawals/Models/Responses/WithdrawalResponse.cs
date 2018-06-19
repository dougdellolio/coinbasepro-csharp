using System;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Withdrawals.Models.Responses
{
    public class WithdrawalResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public DateTime PayoutAt { get; set; }
    }
}
