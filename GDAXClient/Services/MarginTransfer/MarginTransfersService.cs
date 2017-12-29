using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.MarginTransfer.Models;
using Newtonsoft.Json;

namespace GDAXClient.Services.MarginTransfer
{
    public class MarginTransfersService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public MarginTransfersService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)

        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<MarginTransferResponse> CreateMarginTransferAsync(Guid marginProfileId, MarginType type, Currency currency, int amount)
        {
            var newMarginTransfer = JsonConvert.SerializeObject(new Models.MarginTransfer
            {
                margin_profile_id = marginProfileId,
                type = type.ToString().ToLower(),
                currency = currency,
                amount = amount
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator,
                "/profiles/margin-transfer", newMarginTransfer);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var marginTransfer = JsonConvert.DeserializeObject<MarginTransferResponse>(contentBody);

            return marginTransfer;
        }
    }
}
