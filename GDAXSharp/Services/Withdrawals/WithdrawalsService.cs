using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Withdrawals.Models;
using CoinbasePro.Services.Withdrawals.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;

namespace CoinbasePro.Services.Withdrawals
{
    public class WithdrawalsService : AbstractService
    {
        public WithdrawalsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
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

            return await SendServiceCall<WithdrawalResponse>(HttpMethod.Post, "/withdrawals/payment-method", JsonConfig.SerializeObject(newWithdrawal)).ConfigureAwait(false);
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

            return await SendServiceCall<CoinbaseResponse>(HttpMethod.Post, "/withdrawals/coinbase-account", JsonConfig.SerializeObject(newCoinbaseWithdrawal)).ConfigureAwait(false);
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

            return await SendServiceCall<CryptoResponse>(HttpMethod.Post, "/withdrawals/crypto", JsonConfig.SerializeObject(newCryptoWithdrawal)).ConfigureAwait(false);
        }
    }
}
