using GDAXClient.Services.Accounts;
using System.Net.Http;

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
