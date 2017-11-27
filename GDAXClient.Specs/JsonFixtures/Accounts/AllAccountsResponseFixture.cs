namespace GDAXClient.Specs.Fixtures
{
    public static class AllAccountsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    {
        ""id"": ""e316cb9a-0808-4fd7-8914-97829c1925de"",
        ""currency"": ""USD"",
        ""balance"": ""80.2301373066930000"",
        ""available"": ""79.2266348066930000"",
        ""hold"": ""1.0035025000000000"",
        ""margin_enabled"": ""true""
    }
]";

            return json;
        }
    }
}
