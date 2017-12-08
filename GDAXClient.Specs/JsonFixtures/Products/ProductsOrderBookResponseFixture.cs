namespace GDAXClient.Specs.JsonFixtures.Products
{
    public static class ProductsOrderBookResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    'sequence': '3',
    'bids': [
        [200, 100, 3],
    ],
    'asks': [
        [200, 100, 3],
    ]
}";

            return json;
        }
    }
}
