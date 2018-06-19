namespace CoinbasePro.Specs.JsonFixtures.Websocket
{
    public static class WebSocketTypeResponseFixture
    {
        public static string CreateSnapshotResponse()
        {
            return @"
{
    ""type"": ""snapshot"",
    ""product_id"": ""BTC-EUR"",
    ""bids"": [[""1"", ""2""]],
    ""asks"": [[""2"", ""3""]]
}";
        }

        public static string CreateSubscriptionResponse()
        {
            return @"
{
    ""type"": ""subscriptions"",
    ""channels"": [
        {
            ""name"": ""level2"",
            ""product_ids"": [
                ""ETH-USD"",
                ""ETH-EUR""
            ],
        },
        {
            ""name"": ""heartbeat"",
            ""product_ids"": [
                ""ETH-USD"",
                ""ETH-EUR""
            ],
        },
        {
            ""name"": ""ticker"",
            ""product_ids"": [
                ""ETH-USD"",
                ""ETH-EUR"",
                ""ETH-BTC""
            ]
        }
    ]
}";
        }

        public static string CreateTickerResponse()
        {
            return @"
{
    ""type"": ""ticker"",
    ""trade_id"": 20153558,
    ""sequence"": 3262786978,
    ""time"": ""2017-09-02T17:05:49.250000Z"",
    ""product_id"": ""BTC-USD"",
    ""price"": ""4388.01000000"",
    ""side"": ""buy"", // Taker side
    ""last_size"": ""0.03000000"",
    ""best_bid"": ""4388"",
    ""best_ask"": ""4388.01""
}";
        }

        public static string CreateHeartbeatResponse()
        {
            return @"
{
    ""type"": ""heartbeat"",
    ""sequence"": 90,
    ""last_trade_id"": 20,
    ""product_id"": ""BTC-USD"",
    ""time"": ""2014-11-07T08:19:28.464459Z""
}";
        }

        public static string CreateLevel2Response()
        {
            return @"
{
    ""type"": ""l2update"",
    ""product_id"": ""BTC-EUR"",
    ""changes"": [
        [""buy"", ""1"", ""3""],
        [""sell"", ""3"", ""1""],
        [""sell"", ""2"", ""2""],
        [""sell"", ""4"", ""0""]
    ]
}";
        }

        public static string CreateReceivedResponse()
        {
            return @"
{
    ""type"": ""received"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""product_id"": ""BTC-USD"",
    ""sequence"": 10,
    ""order_id"": ""d50ec984-77a8-460a-b958-66f114b0de9b"",
    ""size"": ""1.34"",
    ""price"": ""502.1"",
    ""side"": ""buy"",
    ""order_type"": ""limit""
}";
        }

        public static string CreateOpenResponse()
        {
            return @"
{
    ""type"": ""open"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""product_id"": ""BTC-USD"",
    ""sequence"": 10,
    ""order_id"": ""d50ec984-77a8-460a-b958-66f114b0de9b"",
    ""price"": ""200.2"",
    ""remaining_size"": ""1.00"",
    ""side"": ""sell""
}";
        }

        public static string CreateDoneResponse()
        {
            return @"
{
    ""type"": ""done"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""product_id"": ""BTC-USD"",
    ""sequence"": 10,
    ""price"": ""200.2"",
    ""order_id"": ""d50ec984-77a8-460a-b958-66f114b0de9b"",
    ""reason"": ""filled"", // or ""canceled""
    ""side"": ""sell"",
    ""remaining_size"": ""0""
}";
        }

        public static string CreateMatchResponse()
        {
            return @"
{
    ""type"": ""match"",
    ""trade_id"": 10,
    ""sequence"": 50,
    ""maker_order_id"": ""ac928c66-ca53-498f-9c13-a110027a60e8"",
    ""taker_order_id"": ""132fb6ae-456b-4654-b4e0-d681ac05cea1"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""product_id"": ""BTC-USD"",
    ""size"": ""5.23512"",
    ""price"": ""400.23"",
    ""side"": ""sell""
}";
        }

        public static string CreateActivateResponse()
        {
            return @"
{
    ""type"": ""activate"",
    ""product_id"": ""BTC-USD"",
    ""timestamp"": ""1483736448.299000"",
    ""user_id"": ""12"",
    ""profile_id"": ""30000727-d308-cf50-7b1c-c06deb1934fc"",
    ""order_id"": ""7b52009b-64fd-0a2a-49e6-d8a939753077"",
    ""stop_type"": ""entry"",
    ""side"": ""buy"",
    ""stop_price"": ""80"",
    ""size"": ""2"",
    ""funds"": ""50"",
    ""taker_fee_rate"": ""0.0025"",
    ""private"": true
}";
        }

        public static string CreateChangeResponse(bool withSize)
        {

            return withSize
                ? @"
{
    ""type"": ""change"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""sequence"": 80,
    ""order_id"": ""ac928c66-ca53-498f-9c13-a110027a60e8"",
    ""product_id"": ""BTC-USD"",
    ""new_size"": ""5.23512"",
    ""old_size"": ""12.234412"",
    ""price"": ""400.23"",
    ""side"": ""sell""
}"
                : @"{
    ""type"": ""change"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""sequence"": 80,
    ""order_id"": ""ac928c66-ca53-498f-9c13-a110027a60e8"",
    ""product_id"": ""BTC-USD"",
    ""new_funds"": ""5.23512"",
    ""old_funds"": ""12.234412"",
    ""price"": ""400.23"",
    ""side"": ""sell""
}";

        }

        public static string CreateRandomResponse()
        {
            return @"
{
    ""type"": ""random_type_that_doesnt_exist"",
    ""trade_id"": 10,
    ""sequence"": 50,
    ""maker_order_id"": ""ac928c66-ca53-498f-9c13-a110027a60e8"",
    ""taker_order_id"": ""132fb6ae-456b-4654-b4e0-d681ac05cea1"",
    ""time"": ""2014-11-07T08:19:27.028459Z"",
    ""product_id"": ""BTC-USD"",
    ""size"": ""5.23512"",
    ""price"": ""400.23"",
    ""side"": ""sell""
}";
        }
    }
}