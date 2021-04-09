using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Fills.Models.Responses;

namespace CoinbasePro.Services.Fills
{
    public interface IFillsService
    {
        Task<IList<IList<FillResponse>>> GetFillsByOrderIdAsync(
            string orderId,
            int limit = 100,
            int numberOfPages = 0);

        Task<IList<IList<FillResponse>>> GetFillsByProductIdAsync(
            string productId,
            int limit = 100,
            int numberOfPages = 0);
    }
}
