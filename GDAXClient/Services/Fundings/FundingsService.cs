using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.Fundings.Models;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services.Fundings
{
    public class FundingsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        private readonly IQueryBuilder queryBuilder;

        public FundingsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator,
            IQueryBuilder queryBuilder)
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<IList<Funding>>> GetAllFundingsAsync(int limit = 100, FundingStatus? status = null)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("status", status?.ToString()));

            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<Funding>(HttpMethod.Get, authenticator, $"/funding" + queryString);

            return httpResponseMessage;
        }
    }
}
