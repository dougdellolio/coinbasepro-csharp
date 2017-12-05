namespace GDAXClient.Specs.JsonFixtures.Products
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
    'volume': '2.41000000'
}";

            return json;
        }
    }
}
