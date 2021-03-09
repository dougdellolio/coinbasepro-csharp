using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Deposits.Models;
using CoinbasePro.Services.Deposits.Models.Responses;
using CoinbasePro.Shared.Types;

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
            Currency currency);

        Task<CoinbaseResponse> DepositCoinbaseFundsAsync(
            string coinbaseAccountId,
            decimal amount,
            Currency currency);

        Task<CryptoDepositAddressResponse> GenerateCryptoDepositAddressAsync(string coinbaseAccountId);
    }
}
