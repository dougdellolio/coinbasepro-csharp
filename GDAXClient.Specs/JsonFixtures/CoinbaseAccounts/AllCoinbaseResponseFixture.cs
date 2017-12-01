namespace GDAXClient.Specs.JsonFixtures.Accounts
{
    public static class AllCoinbaseResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
     {
        ""id"": ""fc3a8a57-7142-542d-8436-95a3d82e1622"",
        ""name"": ""ETH Wallet"",
        ""balance"": ""0.00000000"",
        ""currency"": ""ETH"",
        ""type"": ""wallet"",
        ""primary"": false,
        ""active"": true
    },      
    {
        ""id"": ""2ae3354e-f1c3-5771-8a37-6228e9d239db"",
        ""name"": ""USD Wallet"",
        ""balance"": ""0.00"",
        ""currency"": ""USD"",
        ""type"": ""fiat"",
        ""primary"": false,
        ""active"": true,
        ""wire_deposit_information"": {
            ""account_number"": ""0199003122"",
            ""routing_number"": ""026013356"",
            ""bank_name"": ""Metropolitan Commercial Bank"",
            ""bank_address"": ""99 Park Ave 4th Fl New York, NY 10016"",
            ""bank_country"": {
                ""code"": ""US"",
                ""name"": ""United States""
            },
            ""account_name"": ""Coinbase, Inc"",
            ""account_address"": ""548 Market Street, #23008, San Francisco, CA 94104"",
            ""reference"": ""BAOCAEUX""
        }
    },
     {
        ""id"": ""1bfad868-5223-5d3c-8a22-b5ed371e55cb"",
        ""name"": ""BTC Wallet"",
        ""balance"": ""0.00000000"",
        ""currency"": ""BTC"",
        ""type"": ""wallet"",
        ""primary"": true,
        ""active"": true
    },
     {
        ""id"": ""2a11354e-f133-5771-8a37-622be9b239db"",
        ""name"": ""EUR Wallet"",
        ""balance"": ""0.00"",
        ""currency"": ""EUR"",
        ""type"": ""fiat"",
        ""primary"": false,
        ""active"": true,
        ""sepa_deposit_information"": {
            ""iban"": ""EE957700771001355096"",
            ""swift"": ""LHVBEE22"",
            ""bank_name"": ""AS LHV Pank"",
            ""bank_address"": ""Tartu mnt 2, 10145 Tallinn, Estonia"",
            ""bank_country_name"": ""Estonia"",
            ""account_name"": ""Coinbase UK, Ltd."",
            ""account_address"": ""9th Floor, 107 Cheapside, London, EC2V 6DN, United Kingdom"",
            ""reference"": ""CBAEUXOVFXOXYX""
        }
    }
]";

            return json;
        }
    }
}
