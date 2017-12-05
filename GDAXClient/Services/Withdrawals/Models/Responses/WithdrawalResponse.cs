using System;

namespace GDAXClient.Services.Withdrawals
{
    public class WithdrawalResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Payout_at { get; set; }
    }
}
