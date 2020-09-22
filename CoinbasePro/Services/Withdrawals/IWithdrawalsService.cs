using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Withdrawals.Models;
using CoinbasePro.Services.Withdrawals.Models.Responses;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Withdrawals
{
    public interface IWithdrawalsService
    {
        Task<WithdrawalResponse> WithdrawFundsAsync(
            string paymentMethodId,
            decimal amount,
            Currency currency);

        Task<CoinbaseResponse> WithdrawToCoinbaseAsync(
            string coinbaseAccountId,
            decimal amount,
            Currency currency);

        Task<CryptoResponse> WithdrawToCryptoAsync(
            string cryptoAddress,
            decimal amount,
            Currency currency,
            string destinationTag = null);

        Task<IList<Transfer>> GetAllWithdrawals(
            string profileId = null,
            DateTime? before = null,
            DateTime? after = null,
            int limit = 100);

        Task<Transfer> GetWithdrawalById(string transferId);

        Task<FeeEstimateResponse> GetFeeEstimateAsync(
            Currency currency,
            string cryptoAddress);
    }
}
