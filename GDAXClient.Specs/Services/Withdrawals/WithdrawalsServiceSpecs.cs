using System;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Withdrawals;
using GDAXClient.Services.WithdrawalsService;
using GDAXClient.Specs.JsonFixtures.Withdrawals;
using Machine.Fakes;
using Machine.Specifications;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Services;
using GDAXClient.Services.Orders;

namespace GDAXClient.Specs.Services.Withdrawals
{
    [Subject("WithdrawalsService")]
    public class WithdrawalsServiceSpecs : WithSubject<WithdrawalsService>
    {
        static Authenticator authenticator;

        static WithdrawalResponse withdrawals_response;

        static CoinbaseResponse coinbase_response;

        static CryptoResponse crypto_response;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_requesting_a_withdrawal
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(WithdrawalsResponseFixture.Create()));
            };

            Because of = () =>
                withdrawals_response = Subject.WithdrawFundsAsync("593533d2-ff31-46e0-b22e-ca754147a96a", 10, Currency.USD).Result;

            It should_return_a_response = () =>
                withdrawals_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                withdrawals_response.Id.ShouldEqual(new System.Guid("593533d2-ff31-46e0-b22e-ca754147a96a"));
                withdrawals_response.Amount.ShouldEqual(10.00M);
                withdrawals_response.Currency.ShouldEqual("USD");
                withdrawals_response.Payout_at.ShouldEqual(new System.DateTime(2016, 12, 9));
            };
        }

        class when_requesting_coinbase_withdrawal
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>())).Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>())).Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).Return(Task.FromResult(CoinbaseWithdrawalResponseFixture.Create()));
            };

            Because of = () =>
                coinbase_response = Subject.WithdrawToCoinbaseAsync("593533d2-ff31-46e0-b22e-ca754147a96a", 10, Currency.BTC).Result;

            It should_return_a_response = () =>
                coinbase_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                coinbase_response.Id.ShouldEqual(new Guid("593533d2-ff31-46e0-b22e-ca754147a96a"));
                coinbase_response.Amount.ShouldEqual(10.00M);
                coinbase_response.Currency.ShouldEqual("BTC");
            };
        }

        class when_requesting_crypto_withdrawal
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>())).Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>())).Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).Return(Task.FromResult(CryptoWithdrawalResponseFixture.Create()));
            };

            Because of = () =>
                crypto_response = Subject.WithdrawToCryptoAsync("593533d2-ff31-46e0-b22e-ca754147a96a", 10, Currency.BTC).Result;

            It should_return_a_response = () =>
                crypto_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                crypto_response.Id.ShouldEqual(new Guid("593533d2-ff31-46e0-b22e-ca754147a96a"));
                crypto_response.Amount.ShouldEqual(10.00M);
                crypto_response.Currency.ShouldEqual("BTC");
            };
        }
    }
}
