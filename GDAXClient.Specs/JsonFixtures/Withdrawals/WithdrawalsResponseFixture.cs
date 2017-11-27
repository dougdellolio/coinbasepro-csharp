namespace GDAXClient.Specs.JsonFixtures.Withdrawals
{
    public static class WithdrawalsResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""id"":""593533d2-ff31-46e0-b22e-ca754147a96a"",
    ""amount"": ""10.00"",
    ""currency"": ""USD"",
    ""payout_at"": ""2016-12-08T24:00:00Z""
}";

            return json;
        }
    }
}
