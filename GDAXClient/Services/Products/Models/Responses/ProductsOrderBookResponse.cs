using System.Collections.Generic;

namespace GDAXClient.Services.Products.Models.Responses
{
    public class ProductsOrderBookResponse
    {
        public decimal Sequence { get; set; }

        public IEnumerable<IEnumerable<decimal>> Bids { get; set; }

        public IEnumerable<IEnumerable<decimal>> Ask { get; set; }
    }
}
