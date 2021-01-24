using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Withdrawals.Models;
using CoinbasePro.Services.Withdrawals.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;
using CoinbasePro.Shared.Utilities.Queries;

namespace CoinbasePro.Services.Withdrawals
{
    public class WithdrawalsService : AbstractService, IWithdrawalsService
    {
        private readonly QueryBuilder queryBuilder;

        public WithdrawalsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            QueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService)
        {
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<Transfer>> GetAllWithdrawals(
            string profileId = null,
            DateTime? before = null,
            DateTime? after = null,
            int limit = 100)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("type", "withdraw"),
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("profile_id", profileId ?? string.Empty),
                new KeyValuePair<string, string>("before", before.HasValue ? before.Value.ToString("s", CultureInfo.InvariantCulture) : string.Empty),
                new KeyValuePair<string, string>("after", after.HasValue ? after.Value.ToString("s", CultureInfo.InvariantCulture) : string.Empty));

            return await SendServiceCall<IList<Transfer>>(HttpMethod.Get,
                $"/transfers" + queryString).ConfigureAwait(false);
        }

        public async Task<Transfer> GetWithdrawalById(string transferId)
        {
            return await SendServiceCall<Transfer>(HttpMethod.Get, $"/transfers/{transferId}").ConfigureAwait(false);
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
            Currency currency,
            string destinationTag = null)
        {
            var newCryptoWithdrawal = destinationTag == null ? new Crypto
            {
                Amount = amount,
                Currency = currency,
                CryptoAddress = cryptoAddress,
                NoDestinationTag = true
            } : new Crypto
            {
                Amount = amount,
                Currency = currency,
                CryptoAddress = cryptoAddress,
                DestinationTag = destinationTag
            };

            return await SendServiceCall<CryptoResponse>(HttpMethod.Post, "/withdrawals/crypto", JsonConfig.SerializeObject(newCryptoWithdrawal)).ConfigureAwait(false);
        }

        public async Task<FeeEstimateResponse> GetFeeEstimateAsync(
            Currency currency,
            string cryptoAddress)
        {
            var queryString = queryBuilder.BuildQuery(
               new KeyValuePair<string, string>("currency", currency.ToString()),
               new KeyValuePair<string, string>("crypto_address", cryptoAddress));

            return await SendServiceCall<FeeEstimateResponse>(HttpMethod.Get, "/withdrawals/fee-estimate" + queryString).ConfigureAwait(false);
        }
    }
}
