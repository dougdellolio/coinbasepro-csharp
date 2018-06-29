using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Fundings.Models;
using CoinbasePro.Services.Fundings.Types;
using CoinbasePro.Shared.Utilities.Queries;

namespace CoinbasePro.Services.Fundings
{
    public class FundingsService : AbstractService
    {
        private readonly IQueryBuilder queryBuilder;

        public FundingsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IQueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService)
        {
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<IList<Funding>>> GetAllFundingsAsync(
            int limit = 100,
            FundingStatus? status = null,
            int numberOfPages = 0)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("status", status?.ToString().ToLower()));

            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<Funding>(HttpMethod.Get, "/funding" + queryString, numberOfPages: numberOfPages);

            return httpResponseMessage;
        }
    }
}
