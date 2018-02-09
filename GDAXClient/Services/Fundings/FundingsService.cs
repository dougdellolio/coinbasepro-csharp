using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Fundings.Models;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Utilities;

namespace GDAXClient.Services.Fundings
{
    public class FundingsService : AbstractService
    {
        private readonly IAuthenticator authenticator;

        private readonly IQueryBuilder queryBuilder;

        public FundingsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator,
            IQueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService)
        {
            this.authenticator = authenticator;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<IList<Funding>>> GetAllFundingsAsync(int limit = 100, FundingStatus? status = null)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("status", status?.ToString().ToLower()));

            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<Funding>(HttpMethod.Get, authenticator, "/funding" + queryString);

            return httpResponseMessage;
        }
    }
}
