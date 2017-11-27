using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Withdrawals;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services.WithdrawalsService
{
    public class WithdrawalsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public WithdrawalsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<WithdrawalResponse> WithdrawFundsAsync(string paymentMethodId, decimal amount, Currency currency)
        {
            var newWithdrawal = JsonConvert.SerializeObject(new Withdrawal
            {
                amount = amount,
                currency = currency.ToString().ToUpper(),
                payment_method_id = new Guid(paymentMethodId)
            });

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/withdrawals/payment-method", newWithdrawal);
            var withdrawalResponse = JsonConvert.DeserializeObject<WithdrawalResponse>(contentBody);

            return withdrawalResponse;
        }
    }
}
