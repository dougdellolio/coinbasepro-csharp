using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Fees;
using CoinbasePro.Services.Fees.Models;
using CoinbasePro.Specs.JsonFixtures.Network.HttpResponseMessage;
using CoinbasePro.Specs.JsonFixtures.Services.Fees;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Fees
{
    [Subject("FeesService")]
    public class FeesServiceSpecs : WithSubject<FeesService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

        class when_getting_current_fees
        {
            static Fee fee_response;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FeesResponseFixture.Create()));

            Because of = () =>
                fee_response = Subject.GetCurrentFeesAsync().Result;

            It should_return_a_response = () =>
                fee_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fee_response.MakerFeeRate.ShouldEqual(0.0015m);
                fee_response.TakerFeeRate.ShouldEqual(0.0025m);
                fee_response.UsdVolume.ShouldEqual(25000);
            };
        }
    }
}
