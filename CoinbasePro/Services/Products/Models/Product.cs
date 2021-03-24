namespace CoinbasePro.Services.Products.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string BaseCurrency { get; set; }

        public string QuoteCurrency { get; set; }

        public string DisplayName { get; set; }

        public decimal BaseMinSize { get; set; }

        public decimal BaseMaxSize { get; set; }

        public decimal MinMarketFunds { get; set; }

        public decimal MaxMarketFunds { get; set; }

        public decimal QuoteIncrement { get; set; }

        public decimal BaseIncrement { get; set; }

        public bool PostOnly { get; set; }

        public bool LimitOnly { get; set; }

        public bool CancelOnly { get; set; }

        public bool TradingDisabled { get; set; }

        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }
}
