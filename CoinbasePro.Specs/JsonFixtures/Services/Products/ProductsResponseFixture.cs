namespace CoinbasePro.Specs.JsonFixtures.Services.Products
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
        'base_increment': '0.00000001',
        'quote_increment': '0.01',
        'base_min_size': '0.01',
        'base_max_size': '10000.00',
        'min_market_funds': '5',
        'max_market_funds': '1000000',
        'status': 'online',
        'status_message': '',
        'cancel_only': 'false',
        'limit_only': 'false',
        'post_only': 'true',
        'trading_disabled': 'false',
    }
]";

            return json;
        }

        public static string CreateWithUnknownProductType()
        {
            var json = @"
[
    {
        'id': 'UNKNOWN-USD',
        'base_currency': 'unknown',
        'quote_currency': 'unknown',
        'base_min_size': '0.01',
        'base_max_size': '10000.00',
        'quote_increment': '0.01'
    }
]";

            return json;
        }
    }
}
