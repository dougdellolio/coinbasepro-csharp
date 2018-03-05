using Newtonsoft.Json;

namespace GDAXSharp.Services.Currencies.Models
{
    public class Currency
    {
        public Shared.Currency Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("min_size")]
        public decimal MinSize { get; set; }
    }
}
