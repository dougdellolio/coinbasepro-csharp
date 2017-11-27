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

        public async Task<CoinbaseResponse> WithdrawToCoinbaseAsync(string coinbase_account_id, decimal amount, Currency currency)
        {
            var newCoinbaseWithdrawal = JsonConvert.SerializeObject(new Coinbase
            {
                amount = amount,
                currency = currency.ToString().ToUpper(),
                coinbase_account_id = new Guid(coinbase_account_id)
            });

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/withdrawals/coinbase-account", newCoinbaseWithdrawal);
            var coinbaseResponse = JsonConvert.DeserializeObject<CoinbaseResponse>(contentBody);

            return coinbaseResponse;
        }

        public async Task<CryptoResponse> WithdrawToCryptoAsync(string crypto_address, decimal amount, Currency currency)
        {
            var newCryptoWithdrawal = JsonConvert.SerializeObject(new Crypto
            {
                amount = amount,
                currency = currency.ToString().ToUpper(),
                crypto_address = new Guid(crypto_address)
            });

            var contentBody = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/withdrawals/crypto", newCryptoWithdrawal);
            var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(contentBody);

            return cryptoResponse;
        }
    }
}
