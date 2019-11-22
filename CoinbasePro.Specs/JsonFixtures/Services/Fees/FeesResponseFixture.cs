namespace CoinbasePro.Specs.JsonFixtures.Services.Fees
{
    public static class FeesResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""maker_fee_rate"": ""0.0015"",
    ""taker_fee_rate"": ""0.0025"",
    ""usd_volume"": ""25000.00""
}";

            return json;
        }
    }
}
