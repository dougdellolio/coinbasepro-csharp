using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoinbasePro.Network.HttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);

        Task<string> ReadAsStringAsync(HttpResponseMessage httpRequestMessage);
    }
}
