namespace CoinbasePro.Specs.JsonFixtures.Services.Accounts
{
    public static class AccountByIdResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""id"": ""e316cb9a-0808-4fd7-8914-97829c1925de"",
    ""balance"": ""1.100"",
    ""holds"": ""0.100"",
    ""available"": ""1.00"",
    ""currency"": ""USD"",
    ""profile_id"": ""75da88c5-05bf-4f54-bc85-5c775bd68254""
}";

            return json;
        }
    }
}
