using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Reports;
using GDAXSharp.Services.Reports.Models;
using GDAXSharp.Services.Reports.Models.Responses;
using GDAXSharp.Shared;
using GDAXSharp.Specs.JsonFixtures.Reports;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXSharp.Specs.Services.Reports
{
    [Subject("ReportsService")]
    public class ReportsServiceSpecs : WithSubject<ReportsService>
    {
        static Authenticator authenticator;

        static Task<ReportResponse> account_report_response;

        static Task<ReportResponse> fills_report_response;

        Establish context = () =>
        {
            The<IHttpRequestMessageService>().
                WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>()
                    , Param.IsAny<string>(), Param.IsAny<string>())).
                Return(new HttpRequestMessage());

            The<IHttpClient>().
                WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>())).
                Return(Task.FromResult(new HttpResponseMessage()));

            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");
        };

        class when_requesting_new_account_report
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(ReportResponseFixture.Create(ReportType.Account)));

            Because of = () =>
                account_report_response = Subject.CreateNewAccountReportAsync(DateTime.MinValue, DateTime.MaxValue, 
                    "555", ProductType.BtcUsd, "myemail", FileFormat.Csv);

            It should_return_correct_response = () =>
            {

            };
        }

        class when_requesting_new_fills_report
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(ReportResponseFixture.Create(ReportType.Account)));

            Because of = () =>
                account_report_response = Subject.CreateNewFillsReportAsync(DateTime.MinValue, DateTime.MaxValue,
                    ProductType.BtcUsd, "555", "myemail", FileFormat.Csv);

            It should_return_correct_response = () =>
            {

            };
        }
    }
}
