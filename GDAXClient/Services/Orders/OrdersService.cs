using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Utilities.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services.Orders
{
    public class OrdersService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public OrdersService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<OrderResponse> PlaceOrderAsync(OrderSide side, ProductType productId, string type, decimal price, decimal size)
        {
            var newOrder = JsonConvert.SerializeObject(new Order
            {
                side = side.ToString().ToLower(),
                product_id = productId.ToDasherizedUpper(),
                type = type,
                price = price,
                size = size
            });

            var httpRequestMessage = httpRequestMessageService.CreateHttpRequestMessage(HttpMethod.Post, authenticator, "/orders", newOrder);

            var httpResponseMessage = await httpClient.SendASync(httpRequestMessage).ConfigureAwait(false);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);

            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }
    }
}
