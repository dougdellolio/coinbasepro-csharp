using System.Net.Http;
using GDAXSharp.Infrastructure.Authentication;

namespace GDAXSharp.Infrastructure.HttpRequest
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
