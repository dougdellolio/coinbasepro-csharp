namespace CoinbasePro.Specs.JsonFixtures.Services.Limits
{
    public static class LimitsResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
  ""limit_currency"": ""USD"",
  ""transfer_limits"": {
        ""ach"": {
            ""BAT"": {
                ""max"": ""21267.54"",
                ""remaining"": ""21267.54"",
                ""period_in_days"": 7
            }
        },
        ""ach_no_balance"": {
            ""BAT"": {
                ""max"": ""21267.54245368"",
                ""remaining"": ""21267.54245368"",
                ""period_in_days"": 1
            }
        },
        ""credit_debit_card"": {
            ""BAT"": {
                ""max"": ""1450.00481776"",
                ""remaining"": ""1450.00481776"",
                ""period_in_days"": 7
            }
        }
    }
}";

            return json;
        }
    }
}
