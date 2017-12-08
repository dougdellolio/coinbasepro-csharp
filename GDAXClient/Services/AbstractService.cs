using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services
{
    public abstract class AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        protected AbstractService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        protected async Task<HttpResponseMessage> SendHttpRequestMessage(
            HttpMethod httpMethod, 
            IAuthenticator authenticator, 
            string uri, 
            string content = null)
        {
            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri, content);

            var httpResponseMessage = await httpClient.SendASync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(contentBody);
            }

            return httpResponseMessage;
        }

        protected async Task<IEnumerable<IEnumerable<T>>> SendHttpRequestMessageForPagedHeaders<T>(
            HttpMethod httpMethod, 
            IAuthenticator authenticator, 
            string uri, 
            string content = null)
        {
            var pagedList = new List<IEnumerable<T>>();

            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri, content);

            var httpResponseMessage = await httpClient.SendASync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            var firstPage = JsonConvert.DeserializeObject<IEnumerable<T>>(contentBody);
            var firstPageAfterCursorId = httpResponseMessage.Headers.GetValues("cb-after").First();

            pagedList.Add(firstPage);

            var subsequentPageAfterHeaderId = firstPageAfterCursorId;
            HttpResponseMessage subsequentHttpResponseMessage;
            string subsequentContentBody;

            while (true)
            {
                subsequentHttpResponseMessage = await SendHttpRequestMessage(HttpMethod.Get, authenticator, uri + $"&after={subsequentPageAfterHeaderId}");

                if (!subsequentHttpResponseMessage.Headers.TryGetValues("cb-after", out var cursorHeaders))
                {
                    break;
                }

                subsequentPageAfterHeaderId = cursorHeaders.First();
                subsequentContentBody = await httpClient.ReadAsStringAsync(subsequentHttpResponseMessage).ConfigureAwait(false);

                pagedList.Add(JsonConvert.DeserializeObject<IEnumerable<T>>(subsequentContentBody));
            }

            return pagedList;
        }
    }
}
