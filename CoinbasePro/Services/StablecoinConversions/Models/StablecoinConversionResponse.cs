using CoinbasePro.Shared.Types;
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
        public Currency FromCurrency { get; set; }

        [JsonProperty("to")]
        public Currency ToCurrency { get; set; }
    }
}
