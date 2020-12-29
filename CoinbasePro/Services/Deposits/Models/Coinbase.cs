using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Deposits.Models
{
    public class Coinbase
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency Currency { get; set; }

        public string CoinbaseAccountId { get; set; }
    }
}
