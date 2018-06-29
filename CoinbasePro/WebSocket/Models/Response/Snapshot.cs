using System.Collections.Generic;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Snapshot : BaseMessage
    {
        public ProductType ProductId { get; set; }

        public List<decimal[]> Bids { get; set; }

        public List<decimal[]> Asks { get; set; }
    }
}
