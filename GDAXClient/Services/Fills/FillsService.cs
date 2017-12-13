using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.Fills.Models;
using GDAXClient.Services.Fills.Models.Responses;
using GDAXClient.Services.HttpRequest;
using Newtonsoft.Json;

namespace GDAXClient.Services.Fills
{
    public class FillsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator) 
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<IList<IList<FillResponse>>> GetAllFillsAsync(int limit = 100)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Post, authenticator, $"/fills?limit={limit}");

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByOrderIdAsync(string order_id, int limit = 100)
        {
            var fill = JsonConvert.SerializeObject(new Fill
            {
                order_id = new Guid(order_id)
            });

            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Post, authenticator, $"/fills?limit={limit}", fill);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByProductIdAsync(string product_id, int limit = 100)
        {
            var fill = JsonConvert.SerializeObject(new Fill
            {
                product_id = product_id.ToUpper()
            });

            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Post, authenticator, $"/fills?limit={limit}", fill);

            return fills;
        }
    }
}
