using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Deposits.Models;
using GDAXClient.Services.Deposits.Models.Responses;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Withdrawals.Models;
using GDAXClient.Services.Withdrawals.Models.Responses;
using GDAXClient.Shared;
using Newtonsoft.Json;

namespace GDAXClient.Services.Deposits
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
                amount = amount,
                currency = currency.ToString().ToUpper(),
                payment_method_id = new Guid(paymentMethodId)
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
                amount = amount,
                currency = currency.ToString().ToUpper(),
                coinbase_account_id = new Guid(coinbaseAccountId)
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/deposits/coinbase-account", newCoinbaseDeposit);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var depositResponse = JsonConvert.DeserializeObject<CoinbaseResponse>(contentBody);

            return depositResponse;
        }
    }
}
