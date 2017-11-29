namespace GDAXClient.Specs.JsonFixtures.Deposits
{
    public static class DepositsResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
     ""id"": ""593533d2-ff31-46e0-b22e-ca754147a96a"",
    ""amount"": ""10.00"",
    ""currency"": ""USD"",
    ""payout_at"": ""2016-08-20T00:31:09Z""
}";

            return json;
        }
    }
}
