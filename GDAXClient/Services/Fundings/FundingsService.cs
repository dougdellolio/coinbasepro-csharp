using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.Fundings.Models;
using GDAXClient.Services.HttpRequest;
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

        public FundingsService(IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<IList<IList<Funding>>> GetAllFundingsAsync(int limit = 100)
        {
            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<Funding>(HttpMethod.Get, authenticator, $"/");

            return httpResponseMessage;
        }
    }
}
