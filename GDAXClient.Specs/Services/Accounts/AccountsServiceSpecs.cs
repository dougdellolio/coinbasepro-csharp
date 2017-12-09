using System;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Specs.Fixtures;
using GDAXClient.Specs.JsonFixtures;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Services.Accounts.Models;
using GDAXClient.Specs.JsonFixtures.Accounts;
using GDAXClient.Specs.JsonFixtures.HttpResponseMessage;

namespace GDAXClient.Specs.Services.Accounts
{
    [Subject("AccountsService")]
    public class AccountsServiceSpecs : WithSubject<AccountsService>
    {
        static Authenticator authenticator;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_getting_all_accounts
        {
            static IEnumerable<Account> result;

            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AllAccountsResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAllAccountsAsync().Result;

            It should_have_correct_count = () =>
                result.Count().ShouldEqual(1);

            It should_have_correct_account_information = () =>
            {
                result.First().Id.ShouldEqual(new System.Guid("e316cb9a-0808-4fd7-8914-97829c1925de"));
                result.First().Currency.ShouldEqual("USD");
                result.First().Balance.ShouldEqual(80.2301373066930000M);
                result.First().Available.ShouldEqual(79.2266348066930000M);
                result.First().Hold.ShouldEqual(1.0035025000000000M);
                result.First().Margin_enabled.ShouldBeTrue();
            };
        }

        class when_getting_account_by_id
        {
            static Account result;

            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AccountByIdResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAccountByIdAsync("a1b2c3d4").Result;

            It should_have_correct_account_information = () =>
            {
                result.Id.ShouldEqual(new Guid("e316cb9a-0808-4fd7-8914-97829c1925de"));
                result.Currency.ShouldEqual("USD");
                result.Balance.ShouldEqual(1.100M);
                result.Available.ShouldEqual(1.00M);
            };
        }

        class when_getting_account_history
        {
            static IEnumerable<IEnumerable<AccountHistory>> result;

            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());


                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                       .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithAfterValue()));

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));


                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AccountsHistoryResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAccountsHistoryAsync("a1b2c3d4", 1).Result;

            It should_have_correct_account_information = () =>
            {
                result.First().First().Id.ShouldEqual("100");
                result.First().First().Amount.ShouldEqual(0.001M);
                result.First().First().Balance.ShouldEqual(239.669M);
                result.First().First().Type.ShouldEqual("fee");
                result.First().First().Details.Order_id.ShouldEqual("d50ec984-77a8-460a-b958-66f114b0de9b");
                result.First().First().Details.Trade_id.ShouldEqual("74");
                result.First().First().Details.Product_id.ShouldEqual("BTC-USD");
            };
        }
    }
}
