using System.Collections.Generic;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Snapshot : BaseMessage
    {
        public string ProductId { get; set; }

        public List<decimal[]> Bids { get; set; }

        public List<decimal[]> Asks { get; set; }
    }
}
