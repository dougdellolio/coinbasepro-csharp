using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Limits;
using CoinbasePro.Services.Limits.Models;
using CoinbasePro.Specs.JsonFixtures.Network.HttpResponseMessage;
using CoinbasePro.Specs.JsonFixtures.Services.Limits;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Limits
{
    [Subject("LimitsService")]
    public class LimitsServiceSpecs : WithSubject<LimitsService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

        class when_getting_current_exchange_limits
        {
            static Limit limit_response;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(LimitsResponseFixture.Create()));

            Because of = () =>
                limit_response = Subject.GetCurrentExchangeLimitsAsync().Result;

            It should_return_a_response = () =>
                limit_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                limit_response.LimitCurrency.ShouldEqual(CoinbasePro.Shared.Types.Currency.USD);
                limit_response.TransferLimits.Count.ShouldEqual(3);

                limit_response.TransferLimits["ach"]["BAT"].Max.ShouldEqual(21267.54M);
                limit_response.TransferLimits["ach"]["BAT"].Remaining.ShouldEqual(21267.54M);
                limit_response.TransferLimits["ach"]["BAT"].PeriodInDays.ShouldEqual(7);
            };
        }
    }
}
