using System;
using System.Collections.Generic;

namespace GDAXClient.Services.Payments
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public bool Primary_buy { get; set; }

        public bool Primary_sell { get; set; }

        public bool Allow_buy { get; set; }

        public bool Allow_sell { get; set; }

        public bool Allow_deposit { get; set; }

        public bool Allow_withdraw { get; set; }

        public Limit Limits { get; set; }
    }

    public class Limit
    {
        public IEnumerable<BuyPower> Buy { get; set; }

        public IEnumerable<BuyPower> Instant_buy { get; set; }

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
        public decimal Period_in_days { get; set; }

        public Total Total { get; set; }
    }

    public class Total
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
