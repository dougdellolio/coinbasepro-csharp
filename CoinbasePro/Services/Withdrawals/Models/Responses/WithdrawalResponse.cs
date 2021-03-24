using System;

namespace CoinbasePro.Services.Withdrawals.Models.Responses
{
    public class WithdrawalResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime PayoutAt { get; set; }
    }
}
