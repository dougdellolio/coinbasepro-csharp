namespace GDAXClient.Specs.JsonFixtures.MarginTransfers
{
    public static class MarginTransfersResponseFixture
    {
        public static string Create()
        {
            var json = @"
{
  ""created_at"": ""2017-01-25T19:06:23.00Z"",
  ""id"": ""80bc6b74-8b1f-4c60-a089-c61f9810d4ab"",
  ""user_id"": ""521c20b3d4ab09621f000011"",
  ""profile_id"": ""cda95996-ac59-45a3-a42e-30daeb061867"",
  ""margin_profile_id"": ""45fa9e3b-00ba-4631-b907-8a98cbdf21be"",
  ""type"": ""deposit"",
  ""amount"": ""2"",
  ""currency"": ""USD"",
  ""account_id"": ""23035fc7-0707-4b59-b0d2-95d0c035f8f5"",
  ""margin_account_id"": ""e1d9862c-a259-4e83-96cd-376352a9d24d"",
  ""margin_product_id"": ""BTC-USD"",
  ""status"": ""completed"",
  ""nonce"": 25
}";

            return json;
        }
    }
}
