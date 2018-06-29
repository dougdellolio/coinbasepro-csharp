using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Reports;
using CoinbasePro.Services.Reports.Models.Responses;
using CoinbasePro.Services.Reports.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.Reports;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Reports
{
    [Subject("ReportsService")]
    public class ReportsServiceSpecs : WithSubject<ReportsService>
    {
        static ReportResponse account_report_response;

        static ReportResponse fills_report_response;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_requesting_new_account_report
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ReportResponseFixture.Create(ReportType.Account)));

            Because of = () =>
                account_report_response = Subject.CreateNewAccountReportAsync(DateTime.MinValue, DateTime.MaxValue,
                    "555", ProductType.BtcUsd, "myemail", FileFormat.Csv).Result;

            It should_return_correct_response = () =>
            {
                account_report_response.Id.ShouldEqual(new Guid("0428b97b-bec1-429e-a94c-59232926778d"));
                account_report_response.Type.ShouldEqual(ReportType.Account);
                account_report_response.Status.ShouldEqual(ReportStatus.Pending);
                account_report_response.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                account_report_response.CompletedAt.ShouldBeNull();
                account_report_response.ExpiresAt.ShouldEqual(new DateTime(2016, 12, 9));
                account_report_response.FileUrl.ShouldBeNull();
                account_report_response.Params.StartDate.ShouldEqual(new DateTime(2016, 12, 9));
                account_report_response.Params.EndDate.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }

        class when_requesting_new_fills_report
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(ReportResponseFixture.Create(ReportType.Fills)));

            Because of = () =>
                fills_report_response = Subject.CreateNewFillsReportAsync(DateTime.MinValue, DateTime.MaxValue,
                    ProductType.BtcUsd, "555", "myemail", FileFormat.Csv).Result;

            It should_return_correct_response = () =>
            {
                fills_report_response.Id.ShouldEqual(new Guid("0428b97b-bec1-429e-a94c-59232926778d"));
                fills_report_response.Type.ShouldEqual(ReportType.Fills);
                fills_report_response.Status.ShouldEqual(ReportStatus.Pending);
                fills_report_response.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                fills_report_response.CompletedAt.ShouldBeNull();
                fills_report_response.ExpiresAt.ShouldEqual(new DateTime(2016, 12, 9));
                fills_report_response.FileUrl.ShouldBeNull();
                fills_report_response.Params.StartDate.ShouldEqual(new DateTime(2016, 12, 9));
                fills_report_response.Params.EndDate.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }
    }
}
