using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Deposits.Models
{
    public class Deposit
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        [JsonProperty("payment_method_id")]
        public Guid PaymentMethodId { get; set; }
    }
}
