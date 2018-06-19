using System;
using System.Collections.Generic;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Level2 : BaseMessage
    {
        public ProductType ProductId { get; set; }

        public DateTimeOffset Time { get; set; }

        public List<string[]> Changes { get; set; }
    }
}
