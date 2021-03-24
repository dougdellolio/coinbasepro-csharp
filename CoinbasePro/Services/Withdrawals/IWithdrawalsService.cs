using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Withdrawals.Models;
using CoinbasePro.Services.Withdrawals.Models.Responses;

namespace CoinbasePro.Services.Withdrawals
{
    public interface IWithdrawalsService
    {
        Task<WithdrawalResponse> WithdrawFundsAsync(
            string paymentMethodId,
            decimal amount,
            string currency);

        Task<CoinbaseResponse> WithdrawToCoinbaseAsync(
            string coinbaseAccountId,
            decimal amount,
            string currency);

        Task<CryptoResponse> WithdrawToCryptoAsync(
            string cryptoAddress,
            decimal amount,
            string currency,
            string destinationTag = null);

        Task<IList<Transfer>> GetAllWithdrawals(
            string profileId = null,
            DateTime? before = null,
            DateTime? after = null,
            int limit = 100);

        Task<Transfer> GetWithdrawalById(string transferId);

        Task<FeeEstimateResponse> GetFeeEstimateAsync(
            string currency,
            string cryptoAddress);
    }
}
