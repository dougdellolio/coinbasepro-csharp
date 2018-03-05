using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Products.Models
{
    public class Product
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType Id { get; set; }

        [JsonProperty("base_currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency BaseCurrency { get; set; }

        [JsonProperty("quote_currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency QuoteCurrency { get; set; }

        [JsonProperty("base_min_size")]
        public decimal BaseMinSize { get; set; }

        [JsonProperty("base_max_size")]
        public decimal BaseMaxSize { get; set; }

        [JsonProperty("quote_increment")]
        public decimal QuoteIncrement { get; set; }
    }
}
