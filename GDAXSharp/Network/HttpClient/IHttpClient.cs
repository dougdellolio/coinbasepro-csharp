using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GDAXSharp.Infrastructure.HttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);

        Task<string> ReadAsStringAsync(HttpResponseMessage httpRequestMessage);
    }
}
