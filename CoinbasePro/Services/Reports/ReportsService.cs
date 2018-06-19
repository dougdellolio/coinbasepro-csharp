using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Reports.Models;
using CoinbasePro.Services.Reports.Models.Responses;
using CoinbasePro.Services.Reports.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;

namespace CoinbasePro.Services.Reports
{
    public class ReportsService : AbstractService
    {
        public ReportsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<ReportResponse> CreateNewAccountReportAsync(
            DateTime startDate,
            DateTime endDate,
            string accountId,
            ProductType? productType = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf)
        {
            var newReport = JsonConfig.SerializeObject(new Report
            {
                ReportType = ReportType.Account,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = productType,
                AccountId = accountId,
                Format = fileFormat,
                Email = email
            });

            return await CreateReport(newReport);
        }

        public async Task<ReportResponse> CreateNewFillsReportAsync(
            DateTime startDate,
            DateTime endDate,
            ProductType productType,
            string accountId = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf)
        {
            var newReport = JsonConfig.SerializeObject(new Report
            {
                ReportType = ReportType.Fills,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = productType,
                AccountId = accountId,
                Format = fileFormat,
                Email = email
            });

            return await CreateReport(newReport);
        }

        private async Task<ReportResponse> CreateReport(string newReport)
        {
            return await SendServiceCall<ReportResponse>(HttpMethod.Post, "/reports", newReport);
        }
    }
}
