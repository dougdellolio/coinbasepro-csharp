using System;

namespace GDAXClient.Services.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Product_id { get; set; }

        public string Side { get; set; }

        public string Stp { get; set; }

        public string Type { get; set; }

        public string Time_in_force { get; set; }

        public bool Post_only { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Done_at { get; set; }

        public string Done_reason { get; set; }

        public decimal Fill_fees { get; set; }

        public decimal Filled_size { get; set; }

        public decimal Executed_value { get; set; }

        public string Status { get; set; }

        public bool Settled { get; set; }

        public decimal Specified_funds { get; set; }
    }
}
