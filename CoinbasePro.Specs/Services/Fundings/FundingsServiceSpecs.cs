using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Fundings;
using CoinbasePro.Services.Fundings.Models;
using CoinbasePro.Services.Fundings.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Network.HttpResponseMessage;
using CoinbasePro.Specs.JsonFixtures.Services.Fundings;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Fundings
{
    [Subject("FundingsService")]
    public class FundingsServiceSpecs : WithSubject<FundingsService>
    {
        static IList<IList<Funding>> fundings_response;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

        class when_requesting_all_fundings
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FundingsResponseFixture.Create()));

            Because of = () =>
                fundings_response = Subject.GetAllFundingsAsync().Result;

            It should_return_a_response = () =>
                fundings_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fundings_response.First().First().Id.ShouldEqual(new Guid("b93d26cd-7193-4c8d-bfcc-446b2fe18f71"));
                fundings_response.First().First().OrderId.ShouldEqual("b93d26cd-7193-4c8d-bfcc-446b2fe18f71");
                fundings_response.First().First().ProfileId.ShouldEqual("d881e5a6-58eb-47cd-b8e2-8d9f2e3ec6f6");
                fundings_response.First().First().Amount.ShouldEqual(1057.6519956381537500M);
                fundings_response.First().First().Status.ShouldEqual(FundingStatus.Settled);
                fundings_response.First().First().Currency.ShouldEqual(Currency.USD);
                fundings_response.First().First().RepaidAmount.ShouldEqual(1057.6519956381537500M);
                fundings_response.First().First().DefaultAmount.ShouldEqual(0);
                fundings_response.First().First().RepaidDefault.ShouldBeFalse();

                fundings_response.First().Skip(1).First().Id.ShouldEqual(new Guid("280c0a56-f2fa-4d3b-a199-92df76fff5cd"));
                fundings_response.First().Skip(1).First().OrderId.ShouldEqual("280c0a56-f2fa-4d3b-a199-92df76fff5cd");
                fundings_response.First().Skip(1).First().ProfileId.ShouldEqual("d881e5a6-58eb-47cd-b8e2-8d9f2e3ec6f6");
                fundings_response.First().Skip(1).First().Amount.ShouldEqual(545.2400000000000000M);
                fundings_response.First().Skip(1).First().Status.ShouldEqual(FundingStatus.Outstanding);
                fundings_response.First().Skip(1).First().Currency.ShouldEqual(Currency.USD);
                fundings_response.First().Skip(1).First().RepaidAmount.ShouldEqual(532.7580047716682500M);
            };
        }
    }
}
