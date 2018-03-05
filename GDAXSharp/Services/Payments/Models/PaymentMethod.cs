using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Payments.Models
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string PaymentMethodType { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        [JsonProperty("primary_buy")]
        public bool PrimaryBuy { get; set; }

        [JsonProperty("primary_sell")]
        public bool PrimarySell { get; set; }

        [JsonProperty("allow_buy")]
        public bool AllowBuy { get; set; }

        [JsonProperty("allow_sell")]
        public bool AllowSell { get; set; }

        [JsonProperty("allow_deposit")]
        public bool AllowDeposit { get; set; }

        [JsonProperty("allow_withdraw")]
        public bool AllowWithdraw { get; set; }

        public Limit Limits { get; set; }
    }

    public class Limit
    {
        public IEnumerable<BuyPower> Buy { get; set; }

        [JsonProperty("instant_buy")]
        public IEnumerable<BuyPower> InstantBuy { get; set; }

        public IEnumerable<SellPower> Sell { get; set; }

        public IEnumerable<SellPower> Deposit { get; set; }
    }

    public class SellPower : Power
    {
    }

    public class BuyPower : Power
    {
    }

    public abstract class Power
    {
        [JsonProperty("period_in_days")]
        public decimal PeriodInDays { get; set; }

        public Total Total { get; set; }
    }

    public class Total
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }
    }
}
