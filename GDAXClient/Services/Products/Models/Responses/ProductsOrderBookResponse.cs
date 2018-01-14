using System.Collections.Generic;

namespace GDAXClient.Services.Products.Models.Responses
{
    public class ProductsOrderBookResponse
    {
        public ProductsOrderBookResponse(
            decimal sequence,
            IEnumerable<Bid> bids,
            IEnumerable<Ask> asks)
        {
            Sequence = sequence;
            Bids = bids;
            Asks = asks;
        }

        public decimal Sequence { get; }

        public IEnumerable<Bid> Bids { get; }

        public IEnumerable<Ask> Asks { get; }
    }
}
