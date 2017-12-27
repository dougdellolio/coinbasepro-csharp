using System.Collections.Generic;

namespace GDAXClient.WebSocketFeed.Request
{
    public class TickerChannel
    {
        public string type { get; set; }

        public List<string> product_ids { get; set; }

        public object channels { get; set; }
    }
}
