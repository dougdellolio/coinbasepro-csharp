using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Fundings.Models;
using CoinbasePro.Services.Fundings.Types;

namespace CoinbasePro.Services.Fundings
{
    public interface IFundingsService
    {
        Task<IList<IList<Funding>>> GetAllFundingsAsync(
            int limit = 100,
            FundingStatus? status = null,
            int numberOfPages = 0);
    }
}
