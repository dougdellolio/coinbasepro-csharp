using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Currencies;
using GDAXSharp.Specs.JsonFixtures.Services.Currencies;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXSharp.Specs.Services.Currencies
{
    [Subject("CurrenciesService")]
    public class CurrenciesServiceSpecs : WithSubject<CurrenciesService>
    {
        static Authenticator authenticator;

        static IEnumerable<GDAXSharp.Services.Currencies.Models.Currency> result;

        Establish context = () =>
        {
            The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                .Return(new HttpRequestMessage());

            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");
        };

        class when_getting_all_currencies
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CurrenciesResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetAllCurrenciesAsync().Result;

            It should_return_a_correct_number_of_currencies = () =>
                result.Count().ShouldEqual(2);

            It should_return_a_correct_response = () =>
            {
                result.First().Id.ShouldEqual(GDAXSharp.Shared.Types.Currency.BTC);
                result.First().Name.ShouldEqual("Bitcoin");
                result.First().MinSize.ShouldEqual(0.00000001M);
                result.Skip(1).First().Id.ShouldEqual(GDAXSharp.Shared.Types.Currency.USD);
                result.Skip(1).First().Name.ShouldEqual("United States Dollar");
                result.Skip(1).First().MinSize.ShouldEqual(0.01000000M);
            };
        }
    }
}
