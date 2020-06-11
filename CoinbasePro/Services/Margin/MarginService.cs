using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Margin.Models;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Margin
{
    public class MarginService : AbstractService, IMarginService
    {
        public MarginService(
           IHttpClient httpClient,
           IHttpRequestMessageService httpRequestMessageService)
               : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<List<Profile>> GetProfileInformationAsync(ProductType productId)
        {
            return await SendServiceCall<List<Profile>>(HttpMethod.Get, $"/margin/profile_information?product_id={productId.GetEnumMemberValue()}").ConfigureAwait(false);
        }

        public async Task<BuyingSellingPower> GetBuyingPowerAsync(ProductType productId)
        {
            return await SendServiceCall<BuyingSellingPower>(HttpMethod.Get, $"/margin/buying_power?product_id={productId.GetEnumMemberValue()}").ConfigureAwait(false);
        }

        public async Task<List<WithdrawalPowers>> GetWithdrawalPowerAsync(Currency currency)
        {
            return await SendServiceCall<List<WithdrawalPowers>>(HttpMethod.Get, $"/margin/withdrawal_power?currency={currency}").ConfigureAwait(false);
        }

        public async Task<List<WithdrawalPowersAll>> GetAllWithdrawalPowersAsync()
        {
            return await SendServiceCall<List<WithdrawalPowersAll>>(HttpMethod.Get, $"/margin/withdrawal_power_all").ConfigureAwait(false);
        }

        //TODO: no working
        public async Task<ExitPlan> GetExitPlanAsync()
        {
            return await SendServiceCall<ExitPlan>(HttpMethod.Get, $"/margin/exit_plan").ConfigureAwait(false);
        }

        //use after value
        //TODO: no working

        public async Task<List<LiquidationHistory>> GetLiquidationHistoryAsync(DateTime? after = null)
        {
            return await SendServiceCall<List<LiquidationHistory>>(HttpMethod.Get, $"/margin/liquidation_history").ConfigureAwait(false);
        }

        //TODO: no working
        public async Task<PositionRefresh> GetPositionRefreshAmountsAsync()
        {
            return await SendServiceCall<PositionRefresh>(HttpMethod.Get, $"/margin/position_refresh_amounts").ConfigureAwait(false);
        }

        public async Task<MarginStatus> GetMarginStatusAsync()
        {
            return await SendServiceCall<MarginStatus>(HttpMethod.Get, $"/margin/status").ConfigureAwait(false);
        }
    }
}
