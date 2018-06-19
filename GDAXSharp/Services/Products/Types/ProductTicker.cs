using System;

namespace CoinbasePro.Services.Products.Types
{
    public class ProductTicker
    {
        public int TradeId { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public decimal Volume { get; set; }

        public DateTime Time { get; set; }
    }
}
