namespace GDAXClient.Specs.JsonFixtures.Products
{
    public static class ProductTradesResponseFixture
    {
        public static string Create()
        {

            var json = @"
[{
    ""time"": ""2014-11-07T22:19:28.00Z"",
    ""trade_id"": 74,
    ""price"": ""10.00000000"",
    ""size"": ""0.01000000"",
    ""side"": ""buy""
}, {
    ""time"": ""2014-11-07T01:08:43.00Z"",
    ""trade_id"": 73,
    ""price"": ""100.00000000"",
    ""size"": ""0.01000000"",
    ""side"": ""sell""
}]";

            return json;
        }
    }
}
