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

        static Currency single_result;

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
                result.First().Id.ShouldEqual("BTC");
                result.First().Name.ShouldEqual("Bitcoin");
                result.First().MinSize.ShouldEqual(0.00000001M);
                result.Skip(1).First().Id.ShouldEqual("USD");
                result.Skip(1).First().Name.ShouldEqual("United States Dollar");
                result.Skip(1).First().MinSize.ShouldEqual(0.01000000M);
                result.Skip(1).First().Details.Type.ShouldEqual("fiat");
                result.Skip(1).First().Details.NetworkConfirmations.ShouldEqual(0);
                result.Skip(1).First().Details.MinWithdrawalAmount.ShouldEqual(0);
                result.Skip(1).First().Details.MaxWithdrawalAmount.ShouldEqual(0);
                result.Skip(1).First().Details.PushPaymentMethods.ShouldContain("bank_wire");
            };
        }

        class when_getting_a_currency_by_id
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CurrenciesResponseFixture.CreateSingleCurrency()));

            Because of = () =>
                single_result = Subject.GetCurrencyByIdAsync("BTC").Result;

            It should_return_a_correct_response = () =>
            {
                single_result.Id.ShouldEqual("BTC");
                single_result.Name.ShouldEqual("Bitcoin");
                single_result.MinSize.ShouldEqual(0.00000001M);
                single_result.MaxPrecision.ShouldEqual(0.01M);
                single_result.Status.ShouldEqual("online");
                single_result.Details.Type.ShouldEqual("crypto");
                single_result.Details.NetworkConfirmations.ShouldEqual(3);
                single_result.Details.MinWithdrawalAmount.ShouldEqual(0);
                single_result.Details.MaxWithdrawalAmount.ShouldEqual(0);
                single_result.Details.PushPaymentMethods.ShouldContain("crypto");
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
                result.First().Id.ShouldEqual("ETC");
                result.First().Name.ShouldEqual("Ether Classic");
                result.First().MinSize.ShouldEqual(0.00000001M);
            };
        }

        class when_getting_an_unknown_currency
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CurrenciesResponseFixture.CreateUnknown()));

            Because of = () =>
                result = Subject.GetAllCurrenciesAsync().Result;

            It should_return_a_correct_number_of_currencies = () =>
                result.Count().ShouldEqual(1);

            It should_return_a_correct_response = () =>
            {
                result.First().Id.ShouldEqual("UNK");
                result.First().Name.ShouldEqual("Unknown Currency");
                result.First().MinSize.ShouldEqual(0.00000001M);
            };
        }
    }
}
