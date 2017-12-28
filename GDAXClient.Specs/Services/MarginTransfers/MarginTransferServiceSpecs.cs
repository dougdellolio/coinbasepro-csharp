using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.MarginTransfer;
using GDAXClient.Services.MarginTransfer.Models;
using GDAXClient.Services.Orders;
using GDAXClient.Specs.JsonFixtures.MarginTransfers;
using GDAXClient.Utilities.Extensions;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXClient.Specs.Services.MarginTransfers
{
    [Subject("MarginTransferService")]
    public class MarginTransferServiceSpecs : WithSubject<MarginTransferService>
    {
        static MarginTransferResponse margin_transfer_result;

        static Authenticator authenticator;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_creating_margin_transfer
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(MarginTransfersResponseFixture.Create()));
            };

            Because of = () =>
                margin_transfer_result = Subject.CreateMarginTransferAsync(new Guid("45fa9e3b-00ba-4631-b907-8a98cbdf21be"), MarginType.Deposit, Currency.USD, 2).Result;

            It should_return_a_correct_response = () =>
            {
                margin_transfer_result.created_at.ShouldEqual(new DateTime(2017, 01, 25, 19, 06, 23));
                margin_transfer_result.Id.ShouldEqual(new Guid("80bc6b74-8b1f-4c60-a089-c61f9810d4ab"));
                margin_transfer_result.User_id.ShouldEqual("521c20b3d4ab09621f000011");
                margin_transfer_result.Profile_id.ShouldEqual(new Guid("cda95996-ac59-45a3-a42e-30daeb061867"));
                margin_transfer_result.Margin_profile_id.ShouldEqual(new Guid("45fa9e3b-00ba-4631-b907-8a98cbdf21be"));
                margin_transfer_result.Type.ShouldEqual(MarginType.Deposit.ToString().ToLower());
                margin_transfer_result.Amount.ShouldEqual(2);
                margin_transfer_result.Currency.ShouldEqual(Currency.USD);
                margin_transfer_result.Account_id.ShouldEqual(new Guid("23035fc7-0707-4b59-b0d2-95d0c035f8f5"));
                margin_transfer_result.Margin_account_id.ShouldEqual(new Guid("e1d9862c-a259-4e83-96cd-376352a9d24d"));
                margin_transfer_result.Margin_product_id.ShouldEqual(ProductType.BtcUsd.ToDasherizedUpper());
                margin_transfer_result.Status.ShouldEqual("completed");
                margin_transfer_result.Nonce.ShouldEqual(25);
            };
        }
    }
}
