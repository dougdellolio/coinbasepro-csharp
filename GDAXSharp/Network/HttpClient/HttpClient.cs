using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoinbasePro.Network.HttpClient
{
    public class HttpClient : IHttpClient
    {
        private static readonly System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();

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
