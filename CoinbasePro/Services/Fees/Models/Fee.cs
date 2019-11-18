namespace CoinbasePro.Services.Fees.Models
{
    public class Fee
    {
        public decimal MakerFeeRate { get; set; }

        public decimal TakerFeeRate { get; set; }

        public decimal UsdVolume { get; set; }
    }
}
