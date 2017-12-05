namespace GDAXClient.Specs.JsonFixtures.Products
{
    public static class ProductsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    {
        'id': 'BTC-USD',
        'base_currency': 'BTC',
        'quote_currency': 'USD',
        'base_min_size': '0.01',
        'base_max_size': '10000.00',
        'quote_increment': '0.01'
    }
]";

            return json;
        }
    }
}
