using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Reports.Models;
using GDAXSharp.Services.Reports.Models.Responses;
using GDAXSharp.Shared;
using GDAXSharp.Utilities.Extensions;
using Newtonsoft.Json;

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
            var newReport = JsonConvert.SerializeObject(new Report
            {
                type = ReportType.Account.ToString().ToLower(),
                start_date = startDate,
                end_date = endDate,
                product_id = productType?.ToDasherizedUpper(),
                account_id = accountId,
                format = fileFormat.ToString().ToLower(),
                email = email
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/reports", newReport);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contentBody);

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
            var newReport = JsonConvert.SerializeObject(new Report
            {
                type = ReportType.Fills.ToString().ToLower(),
                start_date = startDate,
                end_date = endDate,
                product_id = productType.ToDasherizedUpper(),
                account_id = accountId,
                format = fileFormat.ToString().ToLower(),
                email = email
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/reports", newReport);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contentBody);

            return reportResponse;
        }
    }
}
