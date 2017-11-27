namespace GDAXClient.Specs.JsonFixtures.Payments
{
    public static class PaymentMethodsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    {
        ""id"": ""bc6d7162-d984-5ffa-963c-a493b1c1370b"",
        ""type"": ""ach_bank_account"",
        ""name"": ""Bank of America - eBan... ********7134"",
        ""currency"": ""USD"",
        ""primary_buy"": true,
        ""primary_sell"": true,
        ""allow_buy"": true,
        ""allow_sell"": true,
        ""allow_deposit"": true,
        ""allow_withdraw"": true,
        ""limits"": {
                ""buy"": [
                    {
                    ""period_in_days"": 1,
                        ""total"": {
                        ""amount"": ""10000.00"",
                            ""currency"": ""USD""
                    },
                    ""remaining"": {
                        ""amount"": ""10000.00"",
                        ""currency"": ""USD""
                    }
                }
            ],
            ""instant_buy"": [
                {
                    ""period_in_days"": 7,
                    ""total"": {
                        ""amount"": ""0.00"",
                        ""currency"": ""USD""
                    },
                    ""remaining"": {
                        ""amount"": ""0.00"",
                        ""currency"": ""USD""
                    }
                }
            ],
            ""sell"": [
                {
                    ""period_in_days"": 1,
                    ""total"": {
                        ""amount"": ""10000.00"",
                        ""currency"": ""USD""
                    },
                    ""remaining"": {
                        ""amount"": ""10000.00"",
                        ""currency"": ""USD""
                    }
                }
            ],
            ""deposit"": [
                {
                    ""period_in_days"": 1,
                    ""total"": {
                        ""amount"": ""10000.00"",
                        ""currency"": ""USD""
                    },
                    ""remaining"": {
                        ""amount"": ""10000.00"",
                        ""currency"": ""USD""
                    }
                }
            ]
        }
    },
]";
            return json;
        }
    }
}
