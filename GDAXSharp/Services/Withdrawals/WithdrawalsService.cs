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

        public async Task<WithdrawalResponse> WithdrawFundsAsync(Guid paymentMethodId, decimal amount, Currency currency)
        {
            var newWithdrawal = JsonConvert.SerializeObject(new Withdrawal
            {
                Amount = amount,
                Currency = currency,
                PaymentMethodId = paymentMethodId
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/payment-method", newWithdrawal);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var withdrawalResponse = JsonConvert.DeserializeObject<WithdrawalResponse>(contentBody);

            return withdrawalResponse;
        }

        public async Task<CoinbaseResponse> WithdrawToCoinbaseAsync(Guid coinbaseAccountId, decimal amount, Currency currency)
        {
            var newCoinbaseWithdrawal = JsonConvert.SerializeObject(new Coinbase
            {
                Amount = amount,
                Currency = currency,
                CoinbaseAccountId = coinbaseAccountId
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/coinbase-account", newCoinbaseWithdrawal);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var coinbaseResponse = JsonConvert.DeserializeObject<CoinbaseResponse>(contentBody);

            return coinbaseResponse;
        }

        public async Task<CryptoResponse> WithdrawToCryptoAsync(string cryptoAddress, decimal amount, Currency currency)
        {
            var newCryptoWithdrawal = JsonConvert.SerializeObject(new Crypto
            {
                Amount = amount,
                Currency = currency,
                CryptoAddress = cryptoAddress
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/withdrawals/crypto", newCryptoWithdrawal);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var cryptoResponse = JsonConvert.DeserializeObject<CryptoResponse>(contentBody);

            return cryptoResponse;
        }
    }
}
