using System;

namespace GDAXClient.Services.Products.Models
{
    public class Quote
    {
        public Quote(
            decimal price,
            decimal size)
        {
            Price = price;
            Size = size;
        }

        public decimal Price { get; }

        public decimal Size { get; }

        public decimal? NumberOfOrders { get; set; }

        public Guid? OrderId { get; set; }
    }
}
