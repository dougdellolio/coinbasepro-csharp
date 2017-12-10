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

        protected async Task<HttpResponseMessage> SendHttpRequestMessageAsync(
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

        protected async Task<IList<IList<T>>> SendHttpRequestMessagePagedAsync<T>(
            HttpMethod httpMethod,
            IAuthenticator authenticator,
            string uri,
            string content = null)
        {
            var pagedList = new List<IList<T>>();

            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri, content);

            var httpResponseMessage = await httpClient.SendASync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            var firstPage = JsonConvert.DeserializeObject<IList<T>>(contentBody);

            pagedList.Add(firstPage);

            if (!httpResponseMessage.Headers.TryGetValues("cb-after", out var firstPageAfterCursorId))
            {
                return pagedList;
            }

            var subsequentPages = await GetAllSubsequentPages<T>(uri, firstPageAfterCursorId.First());

            pagedList.AddRange(subsequentPages);

            return pagedList;
        }

        private async Task<IList<IList<T>>> GetAllSubsequentPages<T>(string uri, string firstPageAfterCursorId)
        {
            var pagedList = new List<IList<T>>();
            var subsequentPageAfterHeaderId = firstPageAfterCursorId;

            while (true)
            {
                var subsequentHttpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, uri + $"&after={subsequentPageAfterHeaderId}").ConfigureAwait(false);

                if (!subsequentHttpResponseMessage.Headers.TryGetValues("cb-after", out var cursorHeaders))
                {
                    break;
                }

                subsequentPageAfterHeaderId = cursorHeaders.First();

                var subsequentContentBody = await httpClient.ReadAsStringAsync(subsequentHttpResponseMessage).ConfigureAwait(false);
                var page = JsonConvert.DeserializeObject<IList<T>>(subsequentContentBody);

                pagedList.Add(page);
            }

            return pagedList;
        }
    }
}
