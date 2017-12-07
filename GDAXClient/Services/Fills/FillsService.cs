using System;
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

        public async Task<FillResponse> GetAllFillsAsync()
        {
            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/fills");
            var fills = JsonConvert.DeserializeObject<FillResponse>(contentBody);

            return fills;
        }

        public async Task<FillResponse> GetFillsByOrderIdAsync(string order_id)
        {
            var fill = JsonConvert.SerializeObject(new Fill
            {
                order_id = new Guid(order_id)
            });

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/fills", fill);
            var fills = JsonConvert.DeserializeObject<FillResponse>(contentBody);

            return fills;
        }

        public async Task<FillResponse> GetFillsByProductIdAsync(string product_id)
        {
            var fill = JsonConvert.SerializeObject(new Fill
            {
                product_id = product_id.ToUpper()
            });

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/fills", fill);
            var fills = JsonConvert.DeserializeObject<FillResponse>(contentBody);

            return fills;
        }
    }
}
