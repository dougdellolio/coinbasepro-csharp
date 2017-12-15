namespace GDAXClient.Specs.JsonFixtures.Fills
{
    class FillsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    {
         ""trade_id"": 74,
        ""product_id"": ""BTC-USD"",
        ""price"": ""10.00"",
        ""size"": ""0.01"",
        ""order_id"": ""d50ec984-77a8-460a-b958-66f114b0de9b"",
        ""created_at"": ""2014-11-07T22:19:28.578544Z"",
        ""liquidity"": ""T"",
        ""fee"": ""0.00025"",
        ""settled"": true,
        ""side"": ""buy""
    }
]";

            return json;
        }
    }
}
