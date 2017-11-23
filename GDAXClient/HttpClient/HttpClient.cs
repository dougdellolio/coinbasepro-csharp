using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.HttpClient
{
    public class HttpClient : IHttpClient
    {
        public async Task<HttpResponseMessage> SendASync(HttpRequestMessage httpRequestMessage)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var response = await httpClient.SendAsync(httpRequestMessage);

            return response;
        }
    }
}
