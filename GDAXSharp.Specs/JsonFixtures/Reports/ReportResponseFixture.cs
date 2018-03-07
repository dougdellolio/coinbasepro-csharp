using GDAXSharp.Services.Reports.Models;

namespace GDAXSharp.Specs.JsonFixtures.Reports
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
    ""created_at"": ""2015-01-06T10:34:47.000Z"",
    ""completed_at"": undefined,
    ""expires_at"": ""2015-01-13T10:35:47.000Z"",
    ""file_url"": undefined,
    ""params"": ""{{
        ""start_date"": ""2014-11-01T00:00:00.000Z"",
        ""end_date"": ""2014-11-30T23:59:59.000Z""
    }}
}}";

            return json;
        }
    }
}
