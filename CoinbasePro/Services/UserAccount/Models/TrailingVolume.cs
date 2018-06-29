using System;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.UserAccount.Models
{
    public class TrailingVolume
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        public decimal ExchangeVolume{ get; set; }

        public decimal Volume { get; set; }

        public DateTime RecordedAt{ get; set; }
    }
}
