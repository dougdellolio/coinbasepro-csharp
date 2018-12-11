namespace CoinbasePro.Specs.JsonFixtures.Services.Reports
{
    public static class ReportStatusFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""id"": ""0428b97b-bec1-429e-a94c-59232926778d"",
    ""type"": ""fills"",
    ""status"": ""creating"",
    ""created_at"": ""2016-12-08T24:00:00Z"",
    ""completed_at"": undefined,
    ""expires_at"": ""2016-12-08T24:00:00Z"",
    ""file_url"": undefined,
    ""params"": {
        ""start_date"": ""2016-12-08T24:00:00Z"",
        ""end_date"": ""2016-12-08T24:00:00Z""
    }
}";

            return json;
        }
    }
}
