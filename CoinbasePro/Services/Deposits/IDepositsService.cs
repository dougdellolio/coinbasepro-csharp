using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Deposits.Models;
using CoinbasePro.Services.Deposits.Models.Responses;

namespace CoinbasePro.Services.Deposits
{
    public interface IDepositsService
    {
        Task<IList<Transfer>> GetAllDepositsAsync(
            string profileId = null,
            DateTime? before = null,
            DateTime? after = null,
            int limit = 100);

        Task<Transfer> GetDepositByIdAsync(string transferId);

        Task<DepositResponse> DepositFundsAsync(
            string paymentMethodId,
            decimal amount,
            string currency);

        Task<CoinbaseResponse> DepositCoinbaseFundsAsync(
            string coinbaseAccountId,
            decimal amount,
            string currency);

        Task<CryptoDepositAddressResponse> GenerateCryptoDepositAddressAsync(string coinbaseAccountId);
    }
}
