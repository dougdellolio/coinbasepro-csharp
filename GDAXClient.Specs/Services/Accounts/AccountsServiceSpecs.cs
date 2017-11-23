using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Specs.Fixtures;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Specs.Services.Accounts
{
    [Subject("AccountsService")]
    public class AccountsServiceSpecs : WithSubject<AccountsService>
    {
        static IEnumerable<Account> result;

        static Authenticator authenticator;

        Establish context = () =>
        {
            The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                .Return(new HttpRequestMessage());

            The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

            The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                .Return(Task.FromResult(AccountFixture.Create()));

            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");
        };

        Because of = () =>
            result = Subject.GetAllAccounts(authenticator).Result;

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
}
