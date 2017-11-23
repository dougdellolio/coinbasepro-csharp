using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.HttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendASync(HttpRequestMessage httpRequestMessage);

        Task<string> ReadAsStringAsync(HttpResponseMessage httpRequestMessage);
    }
}
