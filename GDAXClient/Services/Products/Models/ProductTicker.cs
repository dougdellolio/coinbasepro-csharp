using System;

namespace GDAXClient.Services.Products.Models
{
    public class ProductTicker
    {
        public int Trade_id { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public decimal Volume { get; set; }

        public DateTime Time { get; set; }
    }
}
