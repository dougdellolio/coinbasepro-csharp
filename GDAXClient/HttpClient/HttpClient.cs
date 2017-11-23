using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.HttpClient
{
    public class HttpClient : IHttpClient
    {
        public async Task<HttpResponseMessage> SendASync(HttpRequestMessage httpRequestMessage)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var result = await httpClient.SendAsync(httpRequestMessage);
                return result;
            }
        }

        public async Task<string> ReadAsStringAsync(HttpResponseMessage httpRequestMessage)
        {
            var result = await httpRequestMessage.Content.ReadAsStringAsync();
            return result;
        }
    }
}
