using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Withdrawals.Models
{
    public class Coinbase
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public string CoinbaseAccountId { get; set; }
    }
}
