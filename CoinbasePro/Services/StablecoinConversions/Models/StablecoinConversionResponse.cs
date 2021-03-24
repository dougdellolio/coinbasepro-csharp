using System;
using Newtonsoft.Json;

namespace CoinbasePro.Services.StablecoinConversions.Models
{
    public class StablecoinConversionResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string FromAccountId { get; set; }

        public string ToAccountId { get; set; }

        [JsonProperty("from")]
        public string FromCurrency { get; set; }

        [JsonProperty("to")]
        public string ToCurrency { get; set; }
    }
}
