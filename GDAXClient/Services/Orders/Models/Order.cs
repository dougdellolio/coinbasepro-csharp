namespace GDAXClient.Services.Orders.Models
{
    public class Order
    {
        public string side { get; set; }

        public decimal size { get; set; }

        public decimal price { get; set; }

        public string type { get; set; }

        public string product_id { get; set; }

        public string time_in_force { get; set; }

        public string cancel_after { get; set; }

        public bool post_only { get; set; }
    }
}
