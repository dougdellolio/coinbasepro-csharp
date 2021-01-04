using Newtonsoft.Json;

namespace CoinbasePro.Services.Products.Types
{
    public class ProductStats
    {
        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Last { get; set; }

        public decimal Volume { get; set; }

        [JsonProperty("volume_30day")]
        public decimal Volume30Day { get; set; }
    }
}
