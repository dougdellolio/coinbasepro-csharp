using System.Net.Http;
using GDAXClient.Authentication;

namespace GDAXClient.Services.HttpRequest
{
    public interface IHttpRequestMessageService
    {
        HttpRequestMessage CreateHttpRequestMessage(
            HttpMethod httpMethod,
            IAuthenticator authenticator,
            string requestUri,
            string contentBody = "");
    }
}
