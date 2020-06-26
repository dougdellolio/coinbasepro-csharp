using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Margin;
using CoinbasePro.Services.Margin.Models;
using CoinbasePro.Specs.JsonFixtures.Services.Margin;
using CoinbasePro.Shared.Types;
using Machine.Fakes;
using Machine.Specifications;
using System.Linq;
using System;

namespace CoinbasePro.Specs.Services.Margin
{
    [Subject("MarginService")]
    public class MarginServiceSpecs : WithSubject<MarginService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_requesting_margin_profile_information
        {
            static List<Profile> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateMarginProfileInformationResponse()));

            Because of = () =>
                result = Subject.GetProfileInformationAsync(ProductType.BtcUsd).Result;

            It should_return_a_correct_response = () =>
            {
                result.First().ProfileId.ShouldEqual(new Guid("8058d771-2d88-4f0f-ab6e-299c153d4308"));
                result.First().MarginInitialEquity.ShouldEqual(0.33m);
                result.First().MarginWarningEquity.ShouldEqual(0.2m);
                result.First().MarginCallEquity.ShouldEqual(0.15m);
                result.First().EquityPercentage.ShouldEqual(0.8725m);
                result.First().SellingPower.ShouldEqual(0.0022m);
                result.First().BuyingPower.ShouldEqual(23.51m);
                result.First().BorrowPower.ShouldEqual(23.51m);
                result.First().InterestRate.ShouldEqual(0m);
                result.First().InterestPaid.ShouldEqual(0.32m);
                result.First().CollateralCurrencies.Length.ShouldEqual(3);
            };
        }

        class when_requesting_buying_power
        {
            static BuyingSellingPower result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateBuyingPowerResponse()));

            Because of = () =>
                result = Subject.GetBuyingPowerAsync(ProductType.BtcUsd).Result;

            It should_return_a_correct_response = () =>
            {
                result.BuyingPower.ShouldEqual(23.53m);
                result.SellingPower.ShouldEqual(0.0022m);
                result.BuyingPowerExplanation.ShouldEqual("This is the line of credit available to you on the BTC-USD market, given how much collateral assets you currently have in your portfolio.");
            };
        }

        class when_requesting_withdrawal_power
        {
            static List<WithdrawalPowers> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateWithdrawalPowerResponse()));

            Because of = () =>
                result = Subject.GetWithdrawalPowerAsync(Currency.BTC).Result;

            It should_return_a_correct_response = () =>
            {
                result.First().ProfileId.ShouldEqual(new Guid("8058d771-2d88-4f0f-ab6e-299c153d4308"));
                result.First().WithdrawalPower.ShouldEqual(7.775m);
            };
        }

        class when_requesting_all_withdrawal_power
        {
            static List<WithdrawalPowersAll> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateAllWithdrawalPowerResponse()));

            Because of = () =>
                result = Subject.GetAllWithdrawalPowersAsync().Result;

            It should_return_a_correct_response = () =>
            {
                result.First().ProfileId.ShouldEqual(new Guid("8058d771-2d88-4f0f-ab6e-299c153d4308"));
                result.First().MarginableWithdrawalPowers.Count.ShouldEqual(4);
                result.First().MarginableWithdrawalPowers.First().Currency.Equals(Currency.ETH);
            };
        }

        class when_requesting_exit_plan
        {
            static ExitPlan result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateExitPlanResponse()));

            Because of = () =>
                result = Subject.GetExitPlanAsync().Result;

            It should_return_a_correct_response = () =>
            {
                result.ShouldNotBeNull();
            };
        }

        class when_requesting_liquidation_history
        {
            static List<LiquidationHistory> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateLiquidationHistoryResponse()));

            Because of = () =>
                result = Subject.GetLiquidationHistoryAsync().Result;

            It should_return_a_correct_response = () =>
            {
                result.ShouldNotBeNull();
            };
        }

        class when_requesting_position_refresh_amounts
        {
            static PositionRefresh result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreatePositionRefreshAmountResponse()));

            Because of = () =>
                result = Subject.GetPositionRefreshAmountsAsync().Result;

            It should_return_a_correct_response = () =>
            {
                result.ShouldNotBeNull();
            };
        }

        class when_requesting_margin_status
        {
            static MarginStatus result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginResponseFixture.CreateMarginStatusResponse()));

            Because of = () =>
                result = Subject.GetMarginStatusAsync().Result;

            It should_return_a_correct_response = () =>
            {
                result.Eligible.ShouldBeTrue();
                result.Enabled.ShouldBeTrue();
                result.Tier.ShouldEqual(0);
            };
        }
    }
}
