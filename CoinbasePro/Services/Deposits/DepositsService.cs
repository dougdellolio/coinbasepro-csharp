using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Deposits.Models;
using CoinbasePro.Services.Deposits.Models.Responses;
using CoinbasePro.Services.Withdrawals.Models;
using CoinbasePro.Services.Withdrawals.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;

namespace CoinbasePro.Services.Deposits
{
    public class DepositsService : AbstractService
    {
        public DepositsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<DepositResponse> DepositFundsAsync(
            string paymentMethodId,
            decimal amount,
            Currency currency)
        {
            var newDeposit = new Deposit
            {
                Amount = amount,
                Currency = currency,
                PaymentMethodId = new Guid(paymentMethodId)
            };

            return await SendServiceCall<DepositResponse>(HttpMethod.Post, "/deposits/payment-method", JsonConfig.SerializeObject(newDeposit)).ConfigureAwait(false);
        }

        public async Task<CoinbaseResponse> DepositCoinbaseFundsAsync(
            string coinbaseAccountId,
            decimal amount,
            Currency currency)
        {
            var newCoinbaseDeposit = new Coinbase
            {
                Amount = amount,
                Currency = currency,
                CoinbaseAccountId = coinbaseAccountId
            };

            return await SendServiceCall<CoinbaseResponse>(HttpMethod.Post, "/deposits/coinbase-account", JsonConfig.SerializeObject(newCoinbaseDeposit)).ConfigureAwait(false);
        }
    }
}
