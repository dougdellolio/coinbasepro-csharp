using System.Net.Http;
using GDAXSharp.Network.Authentication;

namespace GDAXSharp.Network.HttpRequest
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
