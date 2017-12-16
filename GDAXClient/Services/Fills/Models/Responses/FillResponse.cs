using System;

namespace GDAXClient.Services.Fills.Models.Responses
{
    public class FillResponse
    {
        public int Trade_id { get; set; }

        public string Product_id { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public Guid Order_id { get; set; }

        public DateTime Created_at { get; set; }

        public string Liquidity { get; set; }

        public decimal Fee { get; set; }

        public bool Settled { get; set; }

        public string Side { get; set; }
    }
}
