using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.StablecoinConversions
{
    public class StablecoinConversion
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency From { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency To { get; set; }

        public decimal Amount { get; set; }
    }
}
