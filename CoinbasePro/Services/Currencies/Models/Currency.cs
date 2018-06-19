namespace CoinbasePro.Services.Currencies.Models
{
    public class Currency
    {
        public Shared.Types.Currency Id { get; set; }

        public string Name { get; set; }

        public decimal MinSize { get; set; }
    }
}
