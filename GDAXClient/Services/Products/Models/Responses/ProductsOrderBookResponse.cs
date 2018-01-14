using System.Collections.Generic;

namespace GDAXClient.Services.Products.Models.Responses
{
    public class ProductsOrderBookResponse
    {
        public decimal Sequence { get; set; }

        public IEnumerable<IEnumerable<string>> Bids { get; set; }

        public IEnumerable<IEnumerable<string>> Asks { get; set; }
    }
}
