namespace CoinbasePro.Services.Withdrawals.Models
{
    public class Crypto
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CryptoAddress { get; set; }

        public string DestinationTag { get; set; }

        public bool NoDestinationTag { get; set; }
    }
}
