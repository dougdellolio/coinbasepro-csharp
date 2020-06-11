namespace CoinbasePro.Services.Margin.Models
{
    public class MarginStatus
    {
        public decimal Tier { get; set; }

        public bool Enabled { get; set; }

        public bool Eligible { get; set; }
    }
}
