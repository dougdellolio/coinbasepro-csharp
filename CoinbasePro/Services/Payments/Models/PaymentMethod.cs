using System;
using System.Collections.Generic;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Payments.Models
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string PaymentMethodType { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public bool PrimaryBuy { get; set; }

        public bool PrimarySell { get; set; }

        public bool AllowBuy { get; set; }

        public bool AllowSell { get; set; }

        public bool AllowDeposit { get; set; }

        public bool AllowWithdraw { get; set; }

        public Limit Limits { get; set; }
    }

    public class Limit
    {
        public IEnumerable<BuyPower> Buy { get; set; }

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
        public decimal PeriodInDays { get; set; }

        public Total Total { get; set; }
    }

    public class Total
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
    }
}
