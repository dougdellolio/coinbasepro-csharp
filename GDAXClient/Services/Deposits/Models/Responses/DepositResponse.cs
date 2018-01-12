using System;

namespace GDAXClient.Services.Deposits.Models.Responses
{
    public class DepositResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime Payout_at { get; set; }
    }
}
