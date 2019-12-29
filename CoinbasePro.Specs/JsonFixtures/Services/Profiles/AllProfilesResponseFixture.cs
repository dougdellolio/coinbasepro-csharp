namespace CoinbasePro.Specs.JsonFixtures.Services.Profiles
{
    public static class AllProfilesResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
   {
        ""id"": ""86602c68-306a-4500-ac73-4ce56a91d83c"",
        ""user_id"": ""5844eceecf7e803e259d0365"",
        ""name"": ""default"",
        ""active"": true,
        ""is_default"": true,
        ""created_at"": ""2016-12-08T24:00:00Z""
    }
]";

            return json;
        }
    }
}
