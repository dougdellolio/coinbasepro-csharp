using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Currencies;
using GDAXClient.Services.Currencies.Models;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Specs.JsonFixtures.Currencies;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXClient.Specs.Services.Currencies
{
    [Subject("CurrenciesService")]
    public class CurrenciesServiceSpecs : WithSubject<CurrenciesService>
    {
        static Authenticator authenticator;

        static IEnumerable<Currency> result;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_getting_all_currencies
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CurrenciesResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAllCurrenciesAsync().Result;

            It should_return_a_correct_number_of_currencies = () =>
                result.Count().ShouldEqual(2);

            It should_return_a_correct_response = () =>
            {
                result.First().Id.ShouldEqual("BTC");
                result.First().Name.ShouldEqual("Bitcoin");
                result.First().Min_size.ShouldEqual(0.00000001M);
                result.Skip(1).First().Id.ShouldEqual("USD");
                result.Skip(1).First().Name.ShouldEqual("United States Dollar");
                result.Skip(1).First().Min_size.ShouldEqual(0.01000000M);
            };
        }
    }
}
