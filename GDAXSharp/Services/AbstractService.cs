using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GDAXSharp.Services
{
    public abstract class AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IAuthenticator authenticator;

        private readonly IHttpClient httpClient;

        private JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            FloatParseHandling = FloatParseHandling.Decimal,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        protected AbstractService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.authenticator = authenticator;
            this.httpClient = httpClient;
        }

        protected async Task<HttpResponseMessage> SendHttpRequestMessageAsync(
            HttpMethod httpMethod,
            string uri,
            string content = null)
        {
            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri, content);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(contentBody);
            }

            return httpResponseMessage;
        }

        protected async Task<IList<IList<T>>> SendHttpRequestMessagePagedAsync<T>(
            HttpMethod httpMethod,
            string uri,
            string content = null,
            int numberOfPages = 0)
        {
            var pagedList = new List<IList<T>>();

            var httpRequestMessage = content == null
                ? httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri)
                : httpRequestMessageService.CreateHttpRequestMessage(httpMethod, authenticator, uri, content);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            var firstPage = DeserializeObject<IList<T>>(contentBody);

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
                var subsequentHttpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, uri + $"&after={subsequentPageAfterHeaderId}").ConfigureAwait(false);

                if (!subsequentHttpResponseMessage.Headers.TryGetValues("cb-after", out var cursorHeaders))
                {
                    break;
                }

                subsequentPageAfterHeaderId = cursorHeaders.First();

                var subsequentContentBody = await httpClient.ReadAsStringAsync(subsequentHttpResponseMessage).ConfigureAwait(false);
                var page = DeserializeObject<IList<T>>(subsequentContentBody);

                pagedList.Add(page);

                runCount--;
            }

            return pagedList;
        }

        protected async Task<T> MakeServiceCall<T>(
            HttpMethod httpMethod,
            string uri,
            string content = null)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(httpMethod, uri, content);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            return DeserializeObject<T>(contentBody);
        }

        protected string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, SerializerSettings);
        }

        protected T DeserializeObject<T>(string contentBody)
        {
            return JsonConvert.DeserializeObject<T>(contentBody, SerializerSettings);
        }
    }
}
