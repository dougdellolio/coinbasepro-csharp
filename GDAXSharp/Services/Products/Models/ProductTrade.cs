using System;

namespace GDAXSharp.Services.Products.Models
{
    public class ProductTrade
    {
        public DateTime Time { get; set; }

        public int Trade_id { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Side { get; set; }
    }
}
