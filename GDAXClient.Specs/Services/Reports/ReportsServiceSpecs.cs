using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Reports;
using GDAXClient.Services.Reports.Models;
using GDAXClient.Services.Reports.Models.Responses;
using GDAXClient.Specs.JsonFixtures.Reports;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXClient.Specs.Services.Reports
{
    [Subject("ReportsService")]
    public class ReportsServiceSpecs : WithSubject<ReportsService>
    {
        static Authenticator authenticator;

        static ReportResponse report_response;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_getting_report
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ReportsResponseFixture.Create()));
            };

            Because of = () =>
                report_response = Subject.CreateReportAsync(ReportType.Fills, new DateTime(2014, 11, 01), new DateTime(2014, 11, 30, 23, 59, 59)).Result;

            It should_not_be_null = () =>
                report_response.ShouldNotBeNull();

            It should_have_correct_information = () =>
            {
                report_response.Id.ShouldEqual(new Guid("0428b97b-bec1-429e-a94c-59232926778d"));
                report_response.Type.ShouldEqual(ReportType.Fills.ToString().ToLower());
                report_response.Status.ShouldEqual("pending");
                report_response.Completed_at.ShouldBeNull();
                report_response.File_url.ShouldBeNull();
                report_response.Created_at.ShouldEqual(new DateTime(2015, 01, 06, 10, 34, 47));
                report_response.Expires_at.ShouldEqual(new DateTime(2015, 01, 13, 10, 35, 47));
                report_response.ReportDuration.End_date.ShouldEqual(new DateTime(2014, 11, 30, 23, 59, 59));
                report_response.ReportDuration.Start_date.ShouldEqual(new DateTime(2014, 11, 01));
            };
        }
    }
}
