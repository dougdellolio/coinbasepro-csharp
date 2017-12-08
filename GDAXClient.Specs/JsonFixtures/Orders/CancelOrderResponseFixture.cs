namespace GDAXClient.Specs.JsonFixtures.Orders
{
    public static class CancelOrderResponseFixture
    {
        public static string Create()
        {
            var json = @"
[
    ""144c6f8e-713f-4682-8435-5280fbe8b2b4"",
    ""debe4907-95dc-442f-af3b-cec12f42ebda"",
]";

            return json;
        }

        public static string CreateEmpty()
        {
            var json = @"
{
    ""message"": ""order not found"",
}";

            return json;
        }
    }
}
