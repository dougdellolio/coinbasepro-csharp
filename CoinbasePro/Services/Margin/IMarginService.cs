using CoinbasePro.Services.Margin.Models;
using CoinbasePro.Shared.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Margin
{
    public interface IMarginService
    {
        Task<List<Profile>> GetProfileInformationAsync(ProductType productId);

        Task<BuyingSellingPower> GetBuyingPowerAsync(ProductType productId);

        Task<List<WithdrawalPowers>> GetWithdrawalPowerAsync(Currency currency);

        Task<List<WithdrawalPowersAll>> GetAllWithdrawalPowersAsync();

        Task<ExitPlan> GetExitPlanAsync();

        Task<PositionRefresh> GetPositionRefreshAmountsAsync();

        Task<MarginStatus> GetMarginStatusAsync();

        Task<List<LiquidationHistory>> GetLiquidationHistoryAsync(DateTime? after = null);
    }
}
