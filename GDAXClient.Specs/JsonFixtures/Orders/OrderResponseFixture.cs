namespace GDAXClient.Specs.JsonFixtures.Orders
{
    public static class OrderResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
    ""id"": ""d0c5340b-6d6c-49d9-b567-48c4bfca13d2"",
    ""price"": ""0.10000000"",
    ""size"": ""0.01000000"",
    ""product_id"": ""BTC-USD"",
    ""side"": ""buy"",
    ""stp"": ""dc"",
    ""type"": ""limit"",
    ""time_in_force"": ""GTC"",
    ""post_only"": false,
    ""created_at"": ""2016-12-08T24:00:00Z"",
    ""fill_fees"": ""0.0000000000000000"",
    ""filled_size"": ""0.00000000"",
    ""executed_value"": ""0.0000000000000000"",
    ""status"": ""pending"",
    ""settled"": false
}";

            return json;
        }

        public static string CreateMany()
        {
            var json = @"
[
    {
        ""id"": ""d0c5340b-6d6c-49d9-b567-48c4bfca13d2"",
        ""price"": ""0.10000000"",
        ""size"": ""0.01000000"",
        ""product_id"": ""BTC-USD"",
        ""side"": ""buy"",
        ""stp"": ""dc"",
        ""type"": ""limit"",
        ""time_in_force"": ""GTC"",
        ""post_only"": false,
        ""created_at"": ""2016-12-08T24:00:00Z"",
        ""fill_fees"": ""0.0000000000000000"",
        ""filled_size"": ""0.00000000"",
        ""executed_value"": ""0.0000000000000000"",
        ""status"": ""pending"",
        ""settled"": false
    },
    {    ""id"": ""8b99b139-58f2-4ab2-8e7a-c11c846e3022"",
        ""price"": ""0.10000000"",
        ""size"": ""0.01000000"",
        ""product_id"": ""ETH-USD"",
        ""side"": ""buy"",
        ""stp"": ""dc"",
        ""type"": ""limit"",
        ""time_in_force"": ""GTC"",
        ""post_only"": false,
        ""created_at"": ""2016-12-08T24:00:00Z"",
        ""fill_fees"": ""0.0000000000000000"",
        ""filled_size"": ""0.00000000"",
        ""executed_value"": ""0.0000000000000000"",
        ""status"": ""pending"",
        ""settled"": false
    }
]";

            return json;
        }
    }
}
