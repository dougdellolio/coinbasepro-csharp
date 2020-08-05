using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Withdrawals.Models
{
    public class Crypto
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Currency Currency { get; set; }

        public string CryptoAddress { get; set; }

        public string DestinationTag { get; set; }

        public bool NoDestinationTag { get; set; }
    }
}
