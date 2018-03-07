using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.Deposits;
using GDAXSharp.Services.Deposits.Models.Responses;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Withdrawals.Models.Responses;
using GDAXSharp.Shared;
using GDAXSharp.Specs.JsonFixtures.Deposits;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXSharp.Specs.Services.Deposits
{
    [Subject("DepositsService")]
    public class DepositsServiceSpecs : WithSubject<DepositsService>
    {
        static Authenticator authenticator;

        static DepositResponse deposit_response;

        static CoinbaseResponse coinbase_response;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_requesting_a_deposit
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(DepositsResponseFixture.Create()));
            };

            Because of = () =>
                deposit_response = Subject.DepositFundsAsync("593533d2-ff31-46e0-b22e-ca754147a96a", 10, Currency.USD).Result;

            It should_return_a_response = () =>
                deposit_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                deposit_response.Id.ShouldEqual(new Guid("593533d2-ff31-46e0-b22e-ca754147a96a"));
                deposit_response.Amount.ShouldEqual(10.00M);
                deposit_response.Currency.ShouldEqual(Currency.USD);
                deposit_response.PayoutAt.ShouldEqual(new DateTime(2016, 08, 20, 0, 31, 09));
            };
        }

        class when_requesting_a_coinbase_deposit
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CoinbaseDepositResponseFixture.Create()));
            };

            Because of = () =>
                coinbase_response = Subject.DepositCoinbaseFundsAsync("593533d2-ff31-46e0-b22e-ca754147a96a", 10, Currency.BTC).Result;

            It should_return_a_response = () =>
                coinbase_response.ShouldNotBeNull();

            It should_return_a_correct_respose = () =>
            {
                coinbase_response.Id.ShouldEqual(new Guid("593533d2-ff31-46e0-b22e-ca754147a96a"));
                coinbase_response.Amount.ShouldEqual(10.00M);
                coinbase_response.Currency.ShouldEqual(Currency.BTC);
            };
        }
    }
}
