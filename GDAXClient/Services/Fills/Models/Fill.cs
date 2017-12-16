using System;

namespace GDAXClient.Services.Fills.Models
{
    public class Fill
    {
        public Guid order_id { get; set; }

        public string product_id { get; set; }
    }
}
