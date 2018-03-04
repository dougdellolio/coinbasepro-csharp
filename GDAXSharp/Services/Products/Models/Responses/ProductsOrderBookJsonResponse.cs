using System.Collections.Generic;

namespace GDAXSharp.Services.Products.Models.Responses
{
    public class ProductsOrderBookJsonResponse
    {
        public decimal Sequence { get; set; }

        public IEnumerable<IEnumerable<string>> Bids { get; set; }

        public IEnumerable<IEnumerable<string>> Asks { get; set; }
    }
}
