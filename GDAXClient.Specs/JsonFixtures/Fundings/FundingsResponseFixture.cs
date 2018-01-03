namespace GDAXClient.Specs.JsonFixtures.Fills
{
    class FundingsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
  {
    ""id"": ""b93d26cd-7193-4c8d-bfcc-446b2fe18f71"",
    ""order_id"": ""b93d26cd-7193-4c8d-bfcc-446b2fe18f71"",
    ""profile_id"": ""d881e5a6-58eb-47cd-b8e2-8d9f2e3ec6f6"",
    ""amount"": ""1057.6519956381537500"",
    ""status"": ""settled"",
    ""created_at"": ""2017-03-17T23:46:16.663397Z"",
    ""currency"": ""USD"",
    ""repaid_amount"": ""1057.6519956381537500"",
    ""default_amount"": ""0"",
    ""repaid_default"": false
  },
  {
    ""id"": ""280c0a56-f2fa-4d3b-a199-92df76fff5cd"",
    ""order_id"": ""280c0a56-f2fa-4d3b-a199-92df76fff5cd"",
    ""profile_id"": ""d881e5a6-58eb-47cd-b8e2-8d9f2e3ec6f6"",
    ""amount"": ""545.2400000000000000"",
    ""status"": ""outstanding"",
    ""created_at"": ""2017-03-18T00:34:34.270484Z"",
    ""currency"": ""USD"",
    ""repaid_amount"": ""532.7580047716682500""
  },
]";

            return json;
        }
    }
}
