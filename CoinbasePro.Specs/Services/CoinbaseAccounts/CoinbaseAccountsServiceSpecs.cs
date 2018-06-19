using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.CoinbaseAccounts;
using CoinbasePro.Services.CoinbaseAccounts.Models;
using CoinbasePro.Services.CoinbaseAccounts.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.CoinbaseAccounts;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.CoinbaseAccounts
{
    [Subject("CoinbaseAccountsService")]
    public class CoinbaseAccountsServiceSpecs : WithSubject<CoinbaseAccountsService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_getting_all_coinbase_accounts
        {
            static IEnumerable<CoinbaseAccount> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).Return(Task.FromResult(AllCoinbaseResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetAllAccountsAsync().Result;

            It should_have_correct_count = () =>
                result.Count().ShouldEqual(4);

            It should_have_correct_ETH_account_information = () =>
            {
                result.First().Id.ShouldEqual(new Guid("fc3a8a57-7142-542d-8436-95a3d82e1622"));
                result.First().Name.ShouldEqual("ETH Wallet");
                result.First().Balance.ShouldEqual(0.00000000M);
                result.First().Currency.ShouldEqual(Currency.ETH);
                result.First().CoinbaseAccountType.ShouldEqual(CoinbaseAccountType.Wallet);
                result.First().Primary.ShouldBeFalse();
                result.First().Active.ShouldBeTrue();
            };

            It should_have_correct_US_account_information = () =>
            {
                var usAccount = result.Skip(1).First();

                usAccount.Id.ShouldEqual(new Guid("2ae3354e-f1c3-5771-8a37-6228e9d239db"));
                usAccount.Name.ShouldEqual("USD Wallet");
                usAccount.Balance.ShouldEqual(0.00M);
                usAccount.CoinbaseAccountType.ShouldEqual(CoinbaseAccountType.Fiat);
                usAccount.Primary.ShouldBeFalse();
                usAccount.Active.ShouldBeTrue();
                usAccount.WireDepositInformation.AccountNumber.ShouldEqual("0199003122");
                usAccount.WireDepositInformation.RoutingNumber.ShouldEqual("026013356");
                usAccount.WireDepositInformation.BankName.ShouldEqual("Metropolitan Commercial Bank");
                usAccount.WireDepositInformation.BankAddress.ShouldEqual("99 Park Ave 4th Fl New York, NY 10016");
                usAccount.WireDepositInformation.BankCountry.Code.ShouldEqual("US");
                usAccount.WireDepositInformation.BankCountry.Name.ShouldEqual("United States");
                usAccount.WireDepositInformation.AccountName.ShouldEqual("Coinbase, Inc");
                usAccount.WireDepositInformation.AccountAddress.ShouldEqual(
                    "548 Market Street, #23008, San Francisco, CA 94104");
                usAccount.WireDepositInformation.Reference.ShouldEqual("BAOCAEUX");
            };

            It should_have_corret_BTC_account_information = () =>
            {
                var btcAccount = result.Skip(2).First();

                btcAccount.Id.ShouldEqual(new Guid("1bfad868-5223-5d3c-8a22-b5ed371e55cb"));
                btcAccount.Name.ShouldEqual("BTC Wallet");
                btcAccount.Balance.ShouldEqual(0.00000000M);
                btcAccount.Currency.ShouldEqual(Currency.BTC);
                btcAccount.CoinbaseAccountType.ShouldEqual(CoinbaseAccountType.Wallet);
                btcAccount.Primary.ShouldBeTrue();
                btcAccount.Active.ShouldBeTrue();
            };

            It should_have_correct_EU_account_information = () =>
            {
                var euAccount = result.Last();

                euAccount.Id.ShouldEqual(new Guid("2a11354e-f133-5771-8a37-622be9b239db"));
                euAccount.Name.ShouldEqual("EUR Wallet");
                euAccount.Balance.ShouldEqual(0.00M);
                euAccount.CoinbaseAccountType.ShouldEqual(CoinbaseAccountType.Fiat);
                euAccount.Primary.ShouldBeFalse();
                euAccount.Active.ShouldBeTrue();
                euAccount.SepaDepositInformation.Iban.ShouldEqual("EE957700771001355096");
                euAccount.SepaDepositInformation.Swift.ShouldEqual("LHVBEE22");
                euAccount.SepaDepositInformation.BankName.ShouldEqual("AS LHV Pank");
                euAccount.SepaDepositInformation.BankAddress.ShouldEqual("Tartu mnt 2, 10145 Tallinn, Estonia");
                euAccount.SepaDepositInformation.BankCountryName.ShouldEqual("Estonia");
                euAccount.SepaDepositInformation.AccountName.ShouldEqual("Coinbase UK, Ltd.");
                euAccount.SepaDepositInformation.AccountAddress.ShouldEqual(
                    "9th Floor, 107 Cheapside, London, EC2V 6DN, United Kingdom");
                euAccount.SepaDepositInformation.Reference.ShouldEqual("CBAEUXOVFXOXYX");
            };
        }
    }
}
