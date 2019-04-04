using System.Threading.Tasks;
using CoinbasePro.Services.Deposits.Models.Responses;
using CoinbasePro.Services.Withdrawals.Models.Responses;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Deposits
{
    public interface IDepositsService
    {
        Task<DepositResponse> DepositFundsAsync(
            string paymentMethodId,
            decimal amount,
            Currency currency);

        Task<CoinbaseResponse> DepositCoinbaseFundsAsync(
            string coinbaseAccountId,
            decimal amount,
            Currency currency);
    }
}
