using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Products.Models
{
    public class Product
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency BaseCurrency { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency QuoteCurrency { get; set; }

        public decimal BaseMinSize { get; set; }

        public decimal BaseMaxSize { get; set; }

        public decimal QuoteIncrement { get; set; }
    }
}
