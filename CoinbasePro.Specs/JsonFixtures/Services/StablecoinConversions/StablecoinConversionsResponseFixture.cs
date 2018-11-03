namespace CoinbasePro.Specs.JsonFixtures.Services.StablecoinConversions
{
    public static class StablecoinConversionsResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""id"": ""8942caee-f9d5-4600-a894-4811268545db"",
    ""amount"": ""10000.00"",
    ""from_account_id"": ""7849cc79-8b01-4793-9345-bc6b5f08acce"",
    ""to_account_id"": ""105c3e58-0898-4106-8283-dc5781cda07b"",
    ""from"": ""USD"",
    ""to"": ""USDC""
}";

            return json;
        }
    }
}
