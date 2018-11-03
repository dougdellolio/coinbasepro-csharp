using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.StablecoinConversions
{
    public class StablecoinConversion
    {
        public Currency From { get; set; }

        public Currency To { get; set; }

        public decimal Amount { get; set; }
    }
}
