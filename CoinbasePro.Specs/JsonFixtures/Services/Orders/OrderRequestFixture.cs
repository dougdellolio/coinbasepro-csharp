using System.Net.Http;

namespace CoinbasePro.Specs.JsonFixtures.Services.Orders
{
    public static class OrderRequestFixture
    {
        public static HttpRequestMessage CreateRequest(string content)
        {
            return new HttpRequestMessage(HttpMethod.Post, "")
            {
                Content = new StringContent(content)
            };
        }

        public static string CreateMarketOrderRequest()
        {
            var json = "{\"side\":\"buy\",\"size\":0.01,\"price\":0.0,\"type\":\"market\",\"product_id\":\"BTC-USD\",\"time_in_force\":\"GTC\",\"cancel_after\":\"min\",\"post_only\":false}";

            return json;
        }

        public static string CreateLimitOrderRequest()
        {
            var json = @"{""side"":""buy"",""size"":0.01,""price"":0.1,""type"":""limit"",""product_id"":""BTC-USD"",""time_in_force"":""GTT"",""cancel_after"":""min"",""post_only"":true}";

            return json;
        }

        public static string CreateStopOrderRequest()
        {
            var json = @"{""side"":""buy"",""size"":0.01,""price"":0.1,""type"":""stop"",""product_id"":""BTC-USD"",""time_in_force"":""GTC"",""cancel_after"":""min"",""post_only"":false}";

            return json;
        }

        public static string CreateStopLimitOrderRequest()
        {
            var json = @"{""side"":""buy"",""size"":0.01,""price"":0.1,""stop"":""entry"",""stop_price"":0.1,""type"":""limit"",""product_id"":""BTC-USD"",""time_in_force"":""GTC"",""cancel_after"":""min"",""post_only"":false}";

            return json;
        }
    }
}
