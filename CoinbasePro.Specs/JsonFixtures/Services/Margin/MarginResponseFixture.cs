namespace CoinbasePro.Specs.JsonFixtures.Services.Margin
{
    public static class MarginResponseFixture
    {
        public static string CreateMarginStatusResponse()
        {
            return
@"{
    ""tier"": 0,
    ""enabled"": true,
    ""eligible"": true
}";
        }

        public static string CreateMarginProfileInformationResponse()
        {
            return @"
[{
    ""profile_id"": ""8058d771-2d88-4f0f-ab6e-299c153d4308"",
    ""margin_initial_equity"": ""0.33"",
    ""margin_warning_equity"": ""0.2"",
    ""margin_call_equity"": ""0.15"",
    ""equity_percentage"": 0.8725,
    ""selling_power"": 0.0022,
    ""buying_power"": 23.51,
    ""borrow_power"": 23.51,
    ""interest_rate"": ""0"",
    ""interest_paid"": ""0.32"",
    ""collateral_currencies"": [
        ""BTC"",
        ""USD"",
        ""USDC""
    ],
    ""collateral_hold_value"": ""1.0050000000000000"",
    ""last_liquidation_at"": ""2019-11-21T14:58:49.879Z"",
    ""available_borrow_limits"": {
            ""marginable_limit"": 23.51,
            ""nonmarginable_limit"": 7.75
    },
    ""borrow_limit"": ""5000"",
    ""top_up_amounts"": {
                ""borrowable_usd"": ""0"",
        ""non_borrowable_usd"": ""0""
    }
}]";
        }

        public static string CreateBuyingPowerResponse()
        {
            return @"
{
    ""buying_power"": 23.53,
    ""selling_power"": 0.0022,
    ""buying_power_explanation"": ""This is the line of credit available to you on the BTC-USD market, given how much collateral assets you currently have in your portfolio.""
}";
        }

        public static string CreateWithdrawalPowerResponse()
        {
            return @"
[
    {
        ""profile_id"": ""8058d771-2d88-4f0f-ab6e-299c153d4308"",
        ""withdrawal_power"": ""7.775""
    }
]";
        }

        public static string CreateAllWithdrawalPowerResponse()
        {
            return @"
[
    {
        ""profile_id"": ""8058d771-2d88-4f0f-ab6e-299c153d4308"",
        ""marginable_withdrawal_powers"": [
            {
                ""currency"": ""ETH"",
                ""withdrawal_power"": ""0.01""
            },
            {
                ""currency"": ""BTC"",
                ""withdrawal_power"": ""0.00184821818021342913""
            },
            {
                ""currency"": ""USD"",
                ""withdrawal_power"": ""7.77601796034649750000""
            },
            {
                ""currency"": ""USDC"",
                ""withdrawal_power"": ""1.00332803238200000000""
            }
        ]
    }
]";
        }

        public static string CreateExitPlanResponse()
        {
            return @"
{
    ""id"": ""239f4dc6-72b6-11ea-b311-168e5016c449"",
    ""userId"": ""5cf6e115aaf44503db300f1e"",
    ""profileId"": ""8058d771-2d88-4f0f-ab6e-299c153d4308"",
    ""accountsList"": [
        {
            ""id"": ""434e1152-8eb5-4bfa-89a1-92bb1dcaf0c3"",
            ""currency"": ""BTC"",
            ""amount"": ""0.00221897""
        },
        {
            ""id"": ""6d326768-71f2-4068-99dc-7075c78f6402"",
            ""currency"": ""USD"",
            ""amount"": ""-1.9004458409934425""
        },
        {
                ""id"": ""120c8fcf-94da-4b45-9c43-18f114880f7a"",
            ""currency"": ""USDC"",
            ""amount"": ""1.003328032382""
        }
    ],
    ""equityPercentage"": ""0.8744507743595747"",
    ""totalAssetsUsd"": ""15.137057447382"",
    ""totalLiabilitiesUsd"": ""1.9004458409934425"",
    ""strategiesList"": [
         {
             ""type"": """",
             ""amount"": """",
             ""product"": """",
             ""strategy"": """",
             ""accountId"": """",
             ""orderId"": """"
         }
    ],
    ""createdAt"": ""2020-03-30""
}";
        }

        public static string CreateLiquidationHistoryResponse()
        {
            return @"
[
    {
        ""event_id"": ""6d0edaf1-0c6f-11ea-a88c-0a04debd8c33"",
        ""event_time"": ""2019-11-21T14:58:49.879Z"",
        ""orders"": [
            {
                ""id"": ""6c8d0d4e-0c6f-11ea-947d-0a04debd8c33"",
                ""size"": ""0.02973507"",
                ""product_id"": ""BTC-USD"",
                ""profile_id"": ""8058d771-2d88-4f0f-ab6e-299c153d4308"",
                ""side"": ""sell"",
                ""type"": ""market"",
                ""post_only"": false,
                ""created_at"": ""2019-11-21 14:58:49.582305+00"",
                ""done_at"": ""2019-11-21 14:58:49.596+00"",
                ""done_reason"": ""filled"",
                ""fill_fees"": ""1.1529981537990000"",
                ""filled_size"": ""0.02973507"",
                ""executed_value"": ""230.5996307598000000"",
                ""status"": ""done"",
                ""settled"": true
            }
        ]
    }
]";
        }

        public static string CreatePositionRefreshAmountResponse()
        {
            return @"
{
    ""oneDayRenewalAmount"": ""0"",
    ""twoDayRenewalAmount"": ""417.93""
}";
        }
    }
}
