using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.Deposits.Models;
using GDAXSharp.Services.Deposits.Models.Responses;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Withdrawals.Models;
using GDAXSharp.Services.Withdrawals.Models.Responses;
using GDAXSharp.Shared;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Deposits
{
    public class DepositsService : AbstractService
    {
        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public DepositsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator) 
                : base(httpClient, httpRequestMessageService)
        {
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<DepositResponse> DepositFundsAsync(string paymentMethodId, decimal amount, Currency currency)
        {
            var newDeposit = JsonConvert.SerializeObject(new Deposit
            {
                Amount = amount,
                Currency = currency,
                PaymentMethodId = new Guid(paymentMethodId)
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/deposits/payment-method", newDeposit);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var depositResponse = JsonConvert.DeserializeObject<DepositResponse>(contentBody);

            return depositResponse;
        }

        public async Task<CoinbaseResponse> DepositCoinbaseFundsAsync(string coinbaseAccountId, decimal amount, Currency currency)
        {
            var newCoinbaseDeposit = JsonConvert.SerializeObject(new Coinbase
            {
                Amount = amount,
                Currency = currency,
                CoinbaseAccountId = new Guid(coinbaseAccountId)
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/deposits/coinbase-account", newCoinbaseDeposit);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var depositResponse = JsonConvert.DeserializeObject<CoinbaseResponse>(contentBody);

            return depositResponse;
        }
    }
}
