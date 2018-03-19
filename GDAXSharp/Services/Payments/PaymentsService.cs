using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Payments.Models;

namespace GDAXSharp.Services.Payments
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
