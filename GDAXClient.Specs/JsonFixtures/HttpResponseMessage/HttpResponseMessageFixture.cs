using System.Linq;

namespace GDAXClient.Specs.JsonFixtures.HttpResponseMessage
{
    public static class HttpResponseMessageFixture
    {
        public static System.Net.Http.HttpResponseMessage CreateWithEmptyValue()
        {
            var httpResponseMessage = new System.Net.Http.HttpResponseMessage();
            httpResponseMessage.Headers.Add("cb-after", Enumerable.Empty<string>());

            return httpResponseMessage;
        }

        public static System.Net.Http.HttpResponseMessage CreateWithAfterValue()
        {
            var httpResponseMessage = new System.Net.Http.HttpResponseMessage();
            httpResponseMessage.Headers.Add("cb-after", "100");

            return httpResponseMessage;
        }
    }
}
