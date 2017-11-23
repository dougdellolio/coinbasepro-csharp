using GDAXClient.Authentication;
using System.Net.Http;

namespace GDAXClient.Services.HttpRequest
{
    public interface IHttpRequestMessageService
    {
        HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, Authenticator authenticator, string requestUri, string contentBody = "");
    }
}
