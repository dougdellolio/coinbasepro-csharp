using CoinbasePro.Services.Reports.Types;

namespace CoinbasePro.Specs.JsonFixtures.Services.Reports
{
    public static class ReportResponseFixture
    {
        public static string Create(ReportType reportType)
        {
            var json = $@"
{{
    ""id"": ""0428b97b-bec1-429e-a94c-59232926778d"",
    ""type"": ""{reportType.ToString().ToLower()}"",
    ""status"": ""pending"",
    ""created_at"": ""2016-12-08T24:00:00Z"",
    ""completed_at"": undefined,
    ""expires_at"": ""2016-12-08T24:00:00Z"",
    ""file_url"": undefined,
    ""params"": {{
        ""start_date"": ""2016-12-08T24:00:00Z"",
        ""end_date"": ""2016-12-08T24:00:00Z""
    }}
}}";

            return json;
        }
    }
}
