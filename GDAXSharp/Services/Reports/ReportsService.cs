using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Reports.Models;
using GDAXSharp.Services.Reports.Models.Responses;
using GDAXSharp.Shared;

namespace GDAXSharp.Services.Reports
{
    public class ReportsService : AbstractService
    {
        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public ReportsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService)
        {
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<ReportResponse> CreateNewAccountReportAsync(
            DateTime startDate,
            DateTime endDate,
            string accountId,
            ProductType? productType = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf)
        {
            var newReport = SerializeObject(new Report
            {
                ReportType = ReportType.Account,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = productType,
                AccountId = accountId,
                Format = fileFormat,
                Email = email
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/reports", newReport);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var reportResponse = DeserializeObject<ReportResponse>(contentBody);

            return reportResponse;
        }

        public async Task<ReportResponse> CreateNewFillsReportAsync(
            DateTime startDate,
            DateTime endDate,
            ProductType productType,
            string accountId = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf)
        {
            var newReport = SerializeObject(new Report
            {
                ReportType = ReportType.Fills,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = productType,
                AccountId = accountId,
                Format = fileFormat,
                Email = email
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/reports", newReport);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var reportResponse = DeserializeObject<ReportResponse>(contentBody);

            return reportResponse;
        }
    }
}
