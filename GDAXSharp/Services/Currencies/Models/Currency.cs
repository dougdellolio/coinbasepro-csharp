namespace GDAXSharp.Services.Currencies.Models
{
    public class Currency
    {
        public Shared.Currency Id { get; set; }

        public string Name { get; set; }

        public decimal MinSize { get; set; }
    }
}
