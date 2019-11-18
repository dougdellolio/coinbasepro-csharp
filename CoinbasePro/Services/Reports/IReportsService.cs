using System;
using System.Threading.Tasks;
using CoinbasePro.Services.Reports.Models.Responses;
using CoinbasePro.Services.Reports.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Reports
{
    public interface IReportsService
    {
        Task<ReportResponse> CreateNewAccountReportAsync(
            DateTime startDate,
            DateTime endDate,
            string accountId,
            ProductType? productType = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf);

        Task<ReportResponse> CreateNewFillsReportAsync(
            DateTime startDate,
            DateTime endDate,
            ProductType productType,
            string accountId = null,
            string email = null,
            FileFormat fileFormat = FileFormat.Pdf);

        Task<ReportResponse> GetReportStatus(string id);
    }
}
