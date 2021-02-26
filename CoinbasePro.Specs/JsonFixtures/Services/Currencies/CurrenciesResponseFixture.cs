namespace CoinbasePro.Specs.JsonFixtures.Services.Currencies
{
    public static class CurrenciesResponseFixture
    {
        public static string Create()
        {
            var json = @"
[{
    ""id"": ""BTC"",
    ""name"": ""Bitcoin"",
    ""min_size"": ""0.00000001"",
    ""status"": ""online"",
    ""message"": """",
    ""max_precision"": ""0.01"",
    ""convertible_to"": [],
    ""details"": {
                ""type"": ""crypto"",
        ""symbol"": ""₿"",
        ""network_confirmations"": 3,
        ""sort_order"": 3,
        ""crypto_address_link"": ""https://live.blockcypher.com/btc/address/{{address}}"",
        ""crypto_transaction_link"": ""https://live.blockcypher.com/btc/tx/{{txId}}"",
        ""push_payment_methods"": [
            ""crypto""
        ],
        ""group_types"": [
            ""btc"",
            ""crypto""
        ],
        ""display_name"": """",
        ""processing_time_seconds"": 0,
        ""min_withdrawal_amount"": 0,
        ""max_withdrawal_amount"": 1000
    }
        }, {
    ""id"": ""USD"",
    ""name"": ""United States Dollar"",
    ""min_size"": ""0.01000000"",
    ""status"": ""online"",
    ""message"": """",
    ""max_precision"": ""0.01"",
    ""convertible_to"": [
        ""USDC""
    ],
    ""details"": {
        ""type"": ""fiat"",
        ""symbol"": ""$"",
        ""network_confirmations"": 0,
        ""sort_order"": 0,
        ""crypto_address_link"": """",
        ""crypto_transaction_link"": """",
        ""push_payment_methods"": [
            ""bank_wire"",
            ""fedwire"",
            ""swift_bank_account"",
            ""intra_bank_account""
        ],
        ""group_types"": [
            ""fiat"",
            ""usd""
        ],
        ""display_name"": ""US Dollar"",
        ""processing_time_seconds"": 0,
        ""min_withdrawal_amount"": 0
    }
}]";

            return json;
        }

        public static string CreateEthereumClassic()
        {
            var json = @"
[{
    ""id"": ""ETC"",
    ""name"": ""Ether Classic"",
    ""min_size"": ""0.00000001""
}]";

            return json;
        }

        public static string CreateSingleCurrency()
        {
            var json = @"
{
    ""id"": ""BTC"",
    ""name"": ""Bitcoin"",
    ""min_size"": ""0.00000001"",
    ""status"": ""online"",
    ""max_precision"": ""0.01"",
    ""message"": """",
    ""details"": {
        ""type"": ""crypto"",
        ""symbol"": ""₿"",
        ""network_confirmations"": 3,
        ""sort_order"": 3,
        ""crypto_address_link"": ""https://live.blockcypher.com/btc/address/{{address}}"",
        ""crypto_transaction_link"": ""https://live.blockcypher.com/btc/tx/{{txId}}"",
        ""push_payment_methods"": [
            ""crypto""
        ],
        ""group_types"": [
            ""btc"",
            ""crypto""
        ],
    }
}";

            return json;
        }

        public static string CreateUnknown()
        {
            var json = @"
[{
    ""id"": ""UNK"",
    ""name"": ""Unknown Currency"",
    ""min_size"": ""0.00000001""
}]";

            return json;
        }
    }
}
