using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Accounts;
using CoinbasePro.Services.Accounts.Models;
using CoinbasePro.Services.Accounts.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Network.HttpResponseMessage;
using CoinbasePro.Specs.JsonFixtures.Services.Accounts;

namespace CoinbasePro.Specs.Services.Accounts
{
    [Subject("AccountsService")]
    public class AccountsServiceSpecs : WithSubject<AccountsService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_getting_all_accounts
        {
            static IEnumerable<Account> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AllAccountsResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetAllAccountsAsync().Result;

            It should_have_correct_count = () =>
                result.Count().ShouldEqual(1);

            It should_have_correct_account_information = () =>
            {
                result.First().Id.ShouldEqual(new Guid("e316cb9a-0808-4fd7-8914-97829c1925de"));
                result.First().Currency.ShouldEqual(Currency.USD);
                result.First().Balance.ShouldEqual(80.2301373066930000M);
                result.First().Available.ShouldEqual(79.2266348066930000M);
                result.First().Hold.ShouldEqual(1.0035025000000000M);
                result.First().ProfileId.ShouldEqual(new Guid("75da88c5-05bf-4f54-bc85-5c775bd68254"));
            };
        }

        class when_getting_account_by_id
        {
            static Account result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AccountByIdResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetAccountByIdAsync("a1b2c3d4").Result;

            It should_have_correct_account_information = () =>
            {
                result.Id.ShouldEqual(new Guid("e316cb9a-0808-4fd7-8914-97829c1925de"));
                result.Currency.ShouldEqual(Currency.USD);
                result.Balance.ShouldEqual(1.100M);
                result.Available.ShouldEqual(1.00M);
                result.ProfileId.ShouldEqual(new Guid("75da88c5-05bf-4f54-bc85-5c775bd68254"));
            };
        }

        class when_getting_account_history
        {
            static IList<IList<AccountHistory>> result;

            Establish context = () =>
            {
                The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                       .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AccountsHistoryResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAccountHistoryAsync("a1b2c3d4", 1).Result;

            It should_have_correct_account_information = () =>
            {
                result.First().First().Id.ShouldEqual("100");
                result.First().First().Amount.ShouldEqual(0.001M);
                result.First().First().Balance.ShouldEqual(239.669M);
                result.First().First().AccountEntryType.ShouldEqual(AccountEntryType.Fee);
                result.First().First().Details.OrderId.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
                result.First().First().Details.TradeId.ShouldEqual("74");
                result.First().First().Details.ProductId.ShouldEqual(ProductType.BtcUsd);
            };
        }

        class when_getting_account_holds
        {
            static IList<IList<AccountHold>> result;

            Establish context = () =>
            {
                The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                       .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AccountHoldsResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAccountHoldsAsync("a1b2c3d4", 1).Result;

            It should_have_correct_account_information = () =>
            {
                result.First().First().Id.ShouldEqual("82dcd140-c3c7-4507-8de4-2c529cd1a28f");
                result.First().First().AccountId.ShouldEqual(new Guid("e0b3f39a-183d-453e-b754-0c13e5bab0b3"));
                result.First().First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                result.First().First().UpdatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                result.First().First().Amount.ShouldEqual(4.23M);
                result.First().First().AccountHoldType.ShouldEqual(AccountHoldType.Order);
                result.First().First().Ref.ShouldEqual(new Guid("0a205de4-dd35-4370-a285-fe8fc375a273"));
            };
        }
    }
}
