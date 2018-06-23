using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Exceptions;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Shared.Utilities;

namespace CoinbasePro.Services
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

        private async Task<HttpResponseMessage> SendHttpRequestMessageAsync(
            HttpMethod httpMethod,
            string uri,
            string content = null)
        {
            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, uri, content);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Verbose("@httpResponseMessage", httpResponseMessage);

                return httpResponseMessage;
            }

            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            string errorMessage;

            try
            {
                var jsonMsg = JsonConfig.DeserializeObject<CoinbaseProErrorMessage>(contentBody);
                errorMessage = jsonMsg.Message;
            }
            catch
            {
                errorMessage = contentBody;
            }

            var ex = new CoinbaseProHttpException(errorMessage)
            {
                StatusCode = httpResponseMessage.StatusCode,
                RequestMessage = httpRequestMessage,
                ResponseMessage = httpResponseMessage,
                EndPoint = new EndPoint(httpMethod, uri, content)
            };

            Log.Error("REST request about to throw {@CoinbaseProHttpException}", ex);

            throw ex;
        }

        protected async Task<IList<IList<T>>> SendHttpRequestMessagePagedAsync<T>(
            HttpMethod httpMethod,
            string uri,
            string content = null,
            int numberOfPages = 0)
        {
            Log.Debug("REST {HttpMethod} {Uri} {Content} {NumberOfPages}", httpMethod, uri, content, numberOfPages);

            var pagedList = new List<IList<T>>();

            var httpResponseMessage = await SendHttpRequestMessageAsync(httpMethod, uri, content);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            var firstPage = JsonConfig.DeserializeObject<IList<T>>(contentBody);

            pagedList.Add(firstPage);

            if (!httpResponseMessage.Headers.TryGetValues("cb-after", out var firstPageAfterCursorId))
            {
                return pagedList;
            }

            var subsequentPages = await GetAllSubsequentPages<T>(uri, firstPageAfterCursorId.First(), numberOfPages);

            pagedList.AddRange(subsequentPages);

            return pagedList;
        }

        private async Task<IList<IList<T>>> GetAllSubsequentPages<T>(
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
                Log.Debug("REST {HttpMethod} {Uri} {PageAfter} ", HttpMethod.Get, uri, subsequentPageAfterHeaderId);

                var subsequentHttpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, uri + $"&after={subsequentPageAfterHeaderId}").ConfigureAwait(false);
                if (!subsequentHttpResponseMessage.Headers.TryGetValues("cb-after", out var cursorHeaders))
                {
                    break;
                }

                subsequentPageAfterHeaderId = cursorHeaders.First();

                var subsequentContentBody = await httpClient.ReadAsStringAsync(subsequentHttpResponseMessage).ConfigureAwait(false);
                var page = JsonConfig.DeserializeObject<IList<T>>(subsequentContentBody);

                pagedList.Add(page);

                runCount--;
            }

            return pagedList;
        }

        protected async Task<T> SendServiceCall<T>(
            HttpMethod httpMethod,
            string uri,
            string content = null)
        {
            Log.Debug("REST {HttpMethod} {Uri} {Content}", httpMethod, uri, content);

            var httpResponseMessage = await SendHttpRequestMessageAsync(httpMethod, uri, content);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            return JsonConfig.DeserializeObject<T>(contentBody);
        }
    }
}
