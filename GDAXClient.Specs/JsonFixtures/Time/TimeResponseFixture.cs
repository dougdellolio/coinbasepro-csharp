using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Specs.JsonFixtures.Time
{
    public static class TimeResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""iso"": ""2015-01-07T23:47:25.201Z"",
    ""epoch"": 1420674445.201
}";

            return json;
        }
    }
}
