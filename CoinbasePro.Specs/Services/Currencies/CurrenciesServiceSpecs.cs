using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Currencies;
using CoinbasePro.Services.Currencies.Models;
using CoinbasePro.Specs.JsonFixtures.Services.Currencies;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Currencies
{
    [Subject("CurrenciesService")]
    public class CurrenciesServiceSpecs : WithSubject<CurrenciesService>
    {
        static IEnumerable<Currency> result;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

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
                result.First().Id.ShouldEqual(CoinbasePro.Shared.Types.Currency.BTC);
                result.First().Name.ShouldEqual("Bitcoin");
                result.First().MinSize.ShouldEqual(0.00000001M);
                result.Skip(1).First().Id.ShouldEqual(CoinbasePro.Shared.Types.Currency.USD);
                result.Skip(1).First().Name.ShouldEqual("United States Dollar");
                result.Skip(1).First().MinSize.ShouldEqual(0.01000000M);
            };
        }

        class when_getting_ethereum_classic
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CurrenciesResponseFixture.CreateEthereumClassic()));

            Because of = () =>
                result = Subject.GetAllCurrenciesAsync().Result;

            It should_return_a_correct_number_of_currencies = () =>
                result.Count().ShouldEqual(1);

            It should_return_a_correct_response = () =>
            {
                result.First().Id.ShouldEqual(CoinbasePro.Shared.Types.Currency.ETC);
                result.First().Name.ShouldEqual("Ether Classic");
                result.First().MinSize.ShouldEqual(0.00000001M);
            };
        }
    }
}
