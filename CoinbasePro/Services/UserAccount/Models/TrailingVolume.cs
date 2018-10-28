using System;
using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;

namespace CoinbasePro.Services.UserAccount.Models
{
    public class TrailingVolume
    {
        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public ProductType ProductId { get; set; }

        public decimal ExchangeVolume{ get; set; }

        public decimal Volume { get; set; }

        public DateTime RecordedAt{ get; set; }
    }
}
