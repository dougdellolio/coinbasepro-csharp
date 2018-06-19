namespace CoinbasePro.Specs.JsonFixtures.Services.Products
{
    public static class ProductTradesFixture
    {
        public static string Create()
        {

            var json = @"
[{
    ""time"": ""2014-11-07T22:19:28.578544Z"",
    ""trade_id"": 74,
    ""price"": ""10.00000000"",
    ""size"": ""0.01000000"",
    ""side"": ""buy""
}, {
    ""time"": ""2014-11-07T01:08:43.642366Z"",
    ""trade_id"": 73,
    ""price"": ""100.00000000"",
    ""size"": ""0.01000000"",
    ""side"": ""sell""
}]";

            return json;
        }
    }
}
