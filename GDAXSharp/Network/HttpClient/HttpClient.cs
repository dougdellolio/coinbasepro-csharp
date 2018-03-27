using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GDAXSharp.Network.HttpClient
{
    public class HttpClient : IHttpClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            return await SendAsync(httpRequestMessage, CancellationToken.None);
        }

        public async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage httpRequestMessage,
            CancellationToken cancellationToken)
        {
                var result = await Client.SendAsync(httpRequestMessage, cancellationToken);
                return result;
        }

        public async Task<string> ReadAsStringAsync(HttpResponseMessage httpRequestMessage)
        {
            var result = await httpRequestMessage.Content.ReadAsStringAsync();
            return result;
        }
    }
}
