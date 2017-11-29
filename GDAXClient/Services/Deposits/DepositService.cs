using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Services.Withdrawals;
using Newtonsoft.Json;

namespace GDAXClient.Services.Deposits
{
    public class DepositService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public DepositService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator) 
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
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

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/deposits/payment-method", newDeposit);
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

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/deposits/coinbase-account", newCoinbaseDeposit);
            var depositResponse = JsonConvert.DeserializeObject<CoinbaseResponse>(contentBody);

            return depositResponse;
        }
    }
}
