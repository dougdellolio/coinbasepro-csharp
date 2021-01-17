using System;

namespace CoinbasePro.Services.Withdrawals.Models
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
        public string DestinationTag { get; set; }

        public string SentToAddress { get; set; }

        public Guid CoinbaseAccountId { get; set; }

        public string DestinationTagName { get; set; }

        public string CoinbaseWithdrawalId { get; set; }

        public string CoinbaseTransactionId { get; set; }

        public string CryptoTransactionHash { get; set; }

        public string CoinbasePaymentMethodId { get; set; }

        public decimal Fee { get; set; }

        public decimal Subtotal { get; set; }
    }
}
