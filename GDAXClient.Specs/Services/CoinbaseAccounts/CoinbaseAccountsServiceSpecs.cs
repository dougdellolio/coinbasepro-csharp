using System;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Specs.JsonFixtures.Accounts;
using GDAXClient.Services.CoinbaseAccounts;

namespace GDAXClient.Specs.Services.Accounts
{
    [Subject("CoinbaseAccountsService")]
    public class CoinbaseAccountsServiceSpecs : WithSubject<CoinbaseAccountsService>
    {
        static Authenticator authenticator;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_getting_all_coinbase_accounts
        {
            static IEnumerable<CoinbaseAccount> result;

            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>())).Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>())).Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).Return(Task.FromResult(AllCoinbaseResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.GetAllAccountsAsync().Result;

            It should_have_correct_count = () =>
                result.Count().ShouldEqual(4);

            It should_have_correct_ETH_account_information = () =>
            {
                result.First().Id.ShouldEqual(new Guid("fc3a8a57-7142-542d-8436-95a3d82e1622"));
                result.First().Name.ShouldEqual("ETH Wallet");
                result.First().Balance.ShouldEqual(0.00000000M);
                result.First().Currency.ShouldEqual("ETH");
                result.First().Type.ShouldEqual("wallet");
                result.First().Primary.ShouldBeFalse();
                result.First().Active.ShouldBeTrue();
            };

            It should_have_correct_US_account_information = () =>
            {
                var usAccount = result.Skip(1).First();

                usAccount.Id.ShouldEqual(new Guid("2ae3354e-f1c3-5771-8a37-6228e9d239db"));
                usAccount.Name.ShouldEqual("USD Wallet");
                usAccount.Balance.ShouldEqual(0.00M);
                usAccount.Type.ShouldEqual("fiat");
                usAccount.Primary.ShouldBeFalse();
                usAccount.Active.ShouldBeTrue();
                usAccount.Wire_Deposit_Information.Account_Number.ShouldEqual("0199003122");
                usAccount.Wire_Deposit_Information.Routing_Number.ShouldEqual("026013356");
                usAccount.Wire_Deposit_Information.Bank_Name.ShouldEqual("Metropolitan Commercial Bank");
                usAccount.Wire_Deposit_Information.Bank_Address.ShouldEqual("99 Park Ave 4th Fl New York, NY 10016");
                usAccount.Wire_Deposit_Information.Bank_Country.Code.ShouldEqual("US");
                usAccount.Wire_Deposit_Information.Bank_Country.Name.ShouldEqual("United States");
                usAccount.Wire_Deposit_Information.Account_Name.ShouldEqual("Coinbase, Inc");
                usAccount.Wire_Deposit_Information.Account_Address.ShouldEqual(
                    "548 Market Street, #23008, San Francisco, CA 94104");
                usAccount.Wire_Deposit_Information.Reference.ShouldEqual("BAOCAEUX");
            };

            It should_have_corret_BTC_account_information = () =>
            {
                var btcAccount = result.Skip(2).First();

                btcAccount.Id.ShouldEqual(new Guid("1bfad868-5223-5d3c-8a22-b5ed371e55cb"));
                btcAccount.Name.ShouldEqual("BTC Wallet");
                btcAccount.Balance.ShouldEqual(0.00000000M);
                btcAccount.Currency.ShouldEqual("BTC");
                btcAccount.Type.ShouldEqual("wallet");
                btcAccount.Primary.ShouldBeTrue();
                btcAccount.Active.ShouldBeTrue();
            };

            It should_have_correct_EU_account_information = () =>
            {
                var euAccount = result.Last();

                euAccount.Id.ShouldEqual(new Guid("2a11354e-f133-5771-8a37-622be9b239db"));
                euAccount.Name.ShouldEqual("EUR Wallet");
                euAccount.Balance.ShouldEqual(0.00M);
                euAccount.Type.ShouldEqual("fiat");
                euAccount.Primary.ShouldBeFalse();
                euAccount.Active.ShouldBeTrue();
                euAccount.Sepa_Deposit_Information.Iban.ShouldEqual("EE957700771001355096");
                euAccount.Sepa_Deposit_Information.Swift.ShouldEqual("LHVBEE22");
                euAccount.Sepa_Deposit_Information.Bank_Name.ShouldEqual("AS LHV Pank");
                euAccount.Sepa_Deposit_Information.Bank_Address.ShouldEqual("Tartu mnt 2, 10145 Tallinn, Estonia");
                euAccount.Sepa_Deposit_Information.Bank_Country_Name.ShouldEqual("Estonia");
                euAccount.Sepa_Deposit_Information.Account_Name.ShouldEqual("Coinbase UK, Ltd.");
                euAccount.Sepa_Deposit_Information.Account_Address.ShouldEqual(
                    "9th Floor, 107 Cheapside, London, EC2V 6DN, United Kingdom");
                euAccount.Sepa_Deposit_Information.Reference.ShouldEqual("CBAEUXOVFXOXYX");
            };
        }
    }
}
