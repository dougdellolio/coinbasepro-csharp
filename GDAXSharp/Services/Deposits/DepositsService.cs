using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Deposits.Models;
using GDAXSharp.Services.Deposits.Models.Responses;
using GDAXSharp.Services.Withdrawals.Models;
using GDAXSharp.Services.Withdrawals.Models.Responses;
using GDAXSharp.Shared.Types;

namespace GDAXSharp.Services.Deposits
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

            return await SendServiceCall<DepositResponse>(HttpMethod.Post, "/deposits/payment-method", SerializeObject(newDeposit)).ConfigureAwait(false);
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

            return await SendServiceCall<CoinbaseResponse>(HttpMethod.Post, "/deposits/coinbase-account", SerializeObject(newCoinbaseDeposit)).ConfigureAwait(false);
        }
    }
}
