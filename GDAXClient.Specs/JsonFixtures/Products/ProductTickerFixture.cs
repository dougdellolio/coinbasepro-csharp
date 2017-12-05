namespace GDAXClient.Specs.JsonFixtures.Products
{
    public static class ProductTickerFixture
    {
        public static string Create()
        {
            var json = @"
{
  'trade_id': 4729088,
  'price': '333.99',
  'size': '0.193',
  'bid': '333.98',
  'ask': '333.99',
  'volume': '5957.11914015',
  'time': '2016-12-08T24:00:00Z'
}";

            return json;
        }
    }
}
