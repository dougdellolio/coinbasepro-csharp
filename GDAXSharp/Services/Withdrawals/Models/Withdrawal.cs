using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Withdrawals.Models
{
    public class Withdrawal
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        [JsonProperty("payment_method_id")]
        public Guid PaymentMethodId { get; set; }
    }
}
