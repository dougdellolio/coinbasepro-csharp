using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Withdrawals.Models;
using GDAXSharp.Services.Withdrawals.Models.Responses;
using GDAXSharp.Shared;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Withdrawals
{
    public class WithdrawalsService : AbstractService
    {
        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public WithdrawalsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService)
        {
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<WithdrawalResponse> WithdrawFundsAsync(
            string paymentMethodId, 
            decimal amount, 
            Currency currency)
        {
            var newWithdrawal = new Withdrawal
            {
                Amount = amount,
                Currency = currency,
                PaymentMethodId = paymentMethodId
            };

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/payment-method", SerializeObject(newWithdrawal));
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var withdrawalResponse = DeserializeObject<WithdrawalResponse>(contentBody);

            return withdrawalResponse;
        }

        public async Task<CoinbaseResponse> WithdrawToCoinbaseAsync(
            string coinbaseAccountId, 
            decimal amount,
            Currency currency)
        {
            var newCoinbaseWithdrawal = new Coinbase
            {
                Amount = amount,
                Currency = currency,
                CoinbaseAccountId = coinbaseAccountId
            };

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/coinbase-account", SerializeObject(newCoinbaseWithdrawal));
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var coinbaseResponse = DeserializeObject<CoinbaseResponse>(contentBody);

            return coinbaseResponse;
        }

        public async Task<CryptoResponse> WithdrawToCryptoAsync(
            string cryptoAddress, 
            decimal amount, 
            Currency currency)
        {
            var newCryptoWithdrawal = new Crypto
            {
                Amount = amount,
                Currency = currency,
                CryptoAddress = cryptoAddress
            };

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/crypto", SerializeObject(newCryptoWithdrawal));
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var cryptoResponse = DeserializeObject<CryptoResponse>(contentBody);

            return cryptoResponse;
        }
    }
}
