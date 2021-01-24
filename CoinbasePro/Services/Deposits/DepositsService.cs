using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Deposits.Models;
using CoinbasePro.Services.Deposits.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;
using CoinbasePro.Shared.Utilities.Queries;

namespace CoinbasePro.Services.Deposits
{
    public class DepositsService : AbstractService, IDepositsService
    {
        private readonly QueryBuilder queryBuilder;

        public DepositsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            QueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService)
        {
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<Transfer>> GetAllDepositsAsync(
            string profileId = null,
            DateTime? before = null,
            DateTime? after = null,
            int limit = 100)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("type", "deposit"),
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("profile_id", profileId ?? string.Empty),
                new KeyValuePair<string, string>("before", before.HasValue ? before.Value.ToString("s", CultureInfo.InvariantCulture) : string.Empty),
                new KeyValuePair<string, string>("after", after.HasValue ? after.Value.ToString("s", CultureInfo.InvariantCulture) : string.Empty));

            return await SendServiceCall<IList<Transfer>>(HttpMethod.Get,
                $"/transfers" + queryString).ConfigureAwait(false);
        }

        public async Task<Transfer> GetDepositByIdAsync(string transferId)
        {
            return await SendServiceCall<Transfer>(HttpMethod.Get, $"/transfers/{transferId}").ConfigureAwait(false);
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

        public async Task<CryptoDepositAddressResponse> GenerateCryptoDepositAddressAsync(string coinbaseAccountId)
        {
            return await SendServiceCall<CryptoDepositAddressResponse>(HttpMethod.Post, $"/coinbase-accounts/{coinbaseAccountId}/addresses").ConfigureAwait(false);
        }
    }
}
