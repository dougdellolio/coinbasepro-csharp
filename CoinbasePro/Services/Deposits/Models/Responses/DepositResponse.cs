using System;

namespace CoinbasePro.Services.Deposits.Models.Responses
{
    public class DepositResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime PayoutAt { get; set; }
    }
}
