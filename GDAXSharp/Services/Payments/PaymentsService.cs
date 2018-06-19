using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Payments.Models;

namespace CoinbasePro.Services.Payments
{
    public class PaymentsService : AbstractService
    {
        public PaymentsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await SendServiceCall<IEnumerable<PaymentMethod>>(HttpMethod.Get, "/payment-methods");
        }
    }
}
