using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using Newtonsoft.Json;

namespace GDAXClient.Services
{
    public abstract class AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        protected AbstractService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
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
            string content = null,
            int numberOfPages = 0)
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

            var subsequentPages = await GetAllSubsequentPages<T>(authenticator, uri, firstPageAfterCursorId.First(), numberOfPages);

            pagedList.AddRange(subsequentPages);

            return pagedList;
        }

        private async Task<IList<IList<T>>> GetAllSubsequentPages<T>(
            IAuthenticator authenticator,
            string uri,
            string firstPageAfterCursorId, 
            int numberOfPages)
        {
            var pagedList = new List<IList<T>>();
            var subsequentPageAfterHeaderId = firstPageAfterCursorId;

            var runCount = numberOfPages == 0 
                ? int.MaxValue
                : numberOfPages;

            while (runCount > 1)
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

                runCount--;
            }

            return pagedList;
        }
    }
}
