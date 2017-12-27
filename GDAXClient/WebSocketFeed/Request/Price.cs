using System;

namespace GDAXClient.WebSocketFeed
{
    public class Price
    {
        public string type { get; set; }

        public string Trrade_id { get; set; }

        public long sequence { get; set; }

        public DateTime Time { get; set; }

        public string Product_id { get; set; }

        public decimal PriceUsd { get; set; }

        public string Side { get; set; }

        public decimal Last_size { get; set; }

        public decimal Best_bid { get; set; }

        public decimal Best_ask { get; set; }
    }
}
