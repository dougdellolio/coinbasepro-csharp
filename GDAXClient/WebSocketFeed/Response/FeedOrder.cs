using System;

namespace GDAXClient.WebSocketFeed.Response
{
    public class FeedOrder
    {
        public string Type { get; set; }

        public string Trade_id { get; set; }

        public long Sequence { get; set; }

        public DateTime Time { get; set; }

        public string Product_id { get; set; }

        public decimal Price { get; set; }

        public string Side { get; set; }

        public decimal Last_size { get; set; }

        public decimal Best_bid { get; set; }

        public decimal Best_ask { get; set; }
    }
}
