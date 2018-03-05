using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Withdrawals.Models.Responses
{
    public class CoinbaseResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }
    }
}
