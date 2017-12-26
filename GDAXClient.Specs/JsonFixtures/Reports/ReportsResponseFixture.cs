using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Specs.JsonFixtures.Reports
{
    public class ReportsResponseFixture
    {
        public static string Create()
        {
            var json = @"
    {
        ""id"": ""0428b97b-bec1-429e-a94c-59232926778d"",
        ""type"": ""fills"",
        ""status"": ""pending"",
        ""created_at"": ""2015-01-06T10:34:47.000Z"",
        ""completed_at"": null,
        ""expires_at"": ""2015-01-13T10:35:47.000Z"",
        ""file_url"": null,
        ""params"": {
            ""start_date"": ""2014-11-01T00:00:00.000Z"",
            ""end_date"": ""2014-11-30T23:59:59.000Z""
        }
    }";

            return json;
        }
    }
}
