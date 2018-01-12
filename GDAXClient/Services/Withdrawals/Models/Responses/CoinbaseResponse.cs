using System;

namespace GDAXClient.Services.Withdrawals.Models.Responses
{
    public class CoinbaseResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
