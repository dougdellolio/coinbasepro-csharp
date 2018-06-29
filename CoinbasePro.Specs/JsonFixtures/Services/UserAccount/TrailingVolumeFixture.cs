namespace CoinbasePro.Specs.JsonFixtures.Services.UserAccount
{
    public static class TrailingVolumeFixture
    {
        public static string Create()
        {
            return @"
[
    {
        ""product_id"": ""BTC-USD"",
        ""exchange_volume"": ""11800.00000000"",
        ""volume"": ""100.00000000"",
        ""recorded_at"": ""2016-12-08T24:00:00Z""
    },
    {
        ""product_id"": ""LTC-USD"",
        ""exchange_volume"": ""51010.04100000"",
        ""volume"": ""2010.04100000"",
        ""recorded_at"": ""2016-12-08T24:00:00Z""
    }
]";
        }
    }
}
