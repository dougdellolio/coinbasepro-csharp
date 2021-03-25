using System;
using CoinbasePro.Shared;
using Newtonsoft.Json;

namespace CoinbasePro.Services.UserAccount.Models
{
    public class TrailingVolume
    {
        public string ProductId { get; set; }

        public decimal ExchangeVolume{ get; set; }

        public decimal Volume { get; set; }

        public DateTime RecordedAt{ get; set; }
    }
}
