namespace GDAXClient.Services.Orders
{
    public class Order
    {
        public string side { get; set; }

        public decimal size { get; set; }

        public decimal price { get; set; }

        public string type { get; set; }

        public string product_id { get; set; }
    }
}
