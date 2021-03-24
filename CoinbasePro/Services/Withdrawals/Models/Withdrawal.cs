namespace CoinbasePro.Services.Withdrawals.Models
{
    public class Withdrawal
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string PaymentMethodId { get; set; }
    }
}
