using System.Net.Http;
using GDAXSharp.Authentication;

namespace GDAXSharp.Services.HttpRequest
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
