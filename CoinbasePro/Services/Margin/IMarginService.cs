using CoinbasePro.Services.Margin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Margin
{
    public interface IMarginService
    {
        Task<List<Profile>> GetProfileInformationAsync(string productId);

        Task<BuyingSellingPower> GetBuyingPowerAsync(string productId);

        Task<List<WithdrawalPowers>> GetWithdrawalPowerAsync(string currency);

        Task<List<WithdrawalPowersAll>> GetAllWithdrawalPowersAsync();

        Task<ExitPlan> GetExitPlanAsync();

        Task<PositionRefresh> GetPositionRefreshAmountsAsync();

        Task<MarginStatus> GetMarginStatusAsync();

        Task<List<LiquidationHistory>> GetLiquidationHistoryAsync(DateTime? after = null);
    }
}
