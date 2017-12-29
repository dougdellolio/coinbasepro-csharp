using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Reports.Models;
using GDAXClient.Services.Reports.Models.Responses;
using Newtonsoft.Json;

namespace GDAXClient.Services.Reports
{
    public class ReportsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public ReportsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator) 
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<ReportResponse> CreateReportAsync(ReportType type, DateTime startDate, DateTime endDate, string format = "pdf", string email = null)
        {
            var newReport = JsonConvert.SerializeObject(new Report
            {
                type = type.ToString().ToLower(),
                start_date = startDate,
                end_date = endDate,
                format = format,
                email = email
            });

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/reports", newReport);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);   
            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contentBody, new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Populate
            });

            return reportResponse;
        }

        public async Task<ReportResponse> GetReportStatusAsync(Guid reportId)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, $"/reports/:{reportId.ToString()}");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var reportResponse = JsonConvert.DeserializeObject<ReportResponse>(contentBody);

            return reportResponse;
        }
    }
}
