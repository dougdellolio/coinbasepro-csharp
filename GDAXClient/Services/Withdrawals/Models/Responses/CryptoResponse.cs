using System;

namespace GDAXClient.Services.Withdrawals
{
    public class CryptoResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
