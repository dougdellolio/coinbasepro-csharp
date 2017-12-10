namespace GDAXClient.Specs.JsonFixtures.Accounts
{
    public static class AccountHoldsResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    {
        ""id"": ""82dcd140-c3c7-4507-8de4-2c529cd1a28f"",
        ""account_id"": ""e0b3f39a-183d-453e-b754-0c13e5bab0b3"",
        ""created_at"": ""2016-12-08T24:00:00Z"",
        ""updated_at"": ""2016-12-08T24:00:00Z"",
        ""amount"": ""4.23"",
        ""type"": ""order"",
        ""ref"": ""0a205de4-dd35-4370-a285-fe8fc375a273"",
    }
]";

        return json;
        }
    }
}
