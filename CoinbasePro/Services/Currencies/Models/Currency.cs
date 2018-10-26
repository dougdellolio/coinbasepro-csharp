using CoinbasePro.Shared;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Currencies.Models
{
    public class Currency
    {
        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Shared.Types.Currency Id { get; set; }

        public string Name { get; set; }

        public decimal MinSize { get; set; }
    }
}
