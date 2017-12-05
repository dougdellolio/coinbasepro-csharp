namespace GDAXClient.Services.Products
{
    public class Product
    {
        public string Id { get; set; }

        public string Base_currency { get; set; }

        public string Quote_currency { get; set; }

        public string Base_min_size { get; set; }

        public string Base_max_size { get; set; }

        public string Quote_increment { get; set; }
    }
}
