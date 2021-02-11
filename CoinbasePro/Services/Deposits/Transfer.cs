using System;

namespace CoinbasePro.Services.Deposits.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public DateTime? CanceledAt { get; set; }

        public DateTime ProcessedAt { get; set; }

        public Guid AccountId { get; set; }

        public string UserId { get; set; }

        public string UserNonce { get; set; }

        public decimal Amount { get; set; }

        public Details Details { get; set; }
    }

    public class Details
    {
        public string CryptoAddress { get; set; }

        public string DestinationTag { get; set; }

        public Guid CoinbaseAccountId { get; set; }

        public string DestinationTagName { get; set; }

        public string CryptoTransactionId { get; set; }

        public string CoinbaseTransactionId { get; set; }

        public string CryptoTransactionHash { get; set; }
    }
}
