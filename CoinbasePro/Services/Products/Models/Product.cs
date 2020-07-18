using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Products.Models
{
    public class Product
    {
        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public ProductType Id { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency BaseCurrency { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency QuoteCurrency { get; set; }

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
