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

        public static string CreateWithLevelThree()
        {
            var json = @"
{
    'sequence': '3',
    'bids': [
        [200, 100, '3b0f1225-7f84-490b-a29f-0faef9de823a'],
    ],
    'asks': [
        [200, 100, 'da863862-25f4-4868-ac41-005d11ab0a5f'],
    ]
}";

            return json;
        }
    }
}
