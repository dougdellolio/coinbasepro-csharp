using System;

namespace CoinbasePro.Services.Deposits.Models
{
    public class Deposit
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public Guid PaymentMethodId { get; set; }
    }
}
