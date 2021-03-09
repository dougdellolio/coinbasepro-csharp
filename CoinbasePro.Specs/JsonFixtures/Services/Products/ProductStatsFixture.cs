namespace CoinbasePro.Specs.JsonFixtures.Services.Products
{
    public static class ProductStatsFixture
    {
        public static string Create()
        {

            var json = @"
{
    'open': '34.19000000',
    'high': '95.70000000',
    'low': '7.06000000',
    'volume': '2.41000000',
    'last': '6813.19000000', 
    'volume_30day': '1019451.11188405'
}";

            return json;
        }
    }
}
