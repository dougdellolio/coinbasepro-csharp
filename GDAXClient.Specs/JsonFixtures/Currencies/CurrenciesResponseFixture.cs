namespace GDAXClient.Specs.JsonFixtures.Currencies
{
    public static class CurrenciesResponseFixture
    {
        public static string Create()
        {
            var json = @"
[{
    ""id"": ""BTC"",
    ""name"": ""Bitcoin"",
    ""min_size"": ""0.00000001""
}, {
    ""id"": ""USD"",
    ""name"": ""United States Dollar"",
    ""min_size"": ""0.01000000""
}]";

            return json;
        }
    }
}
