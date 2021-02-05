using CoinbasePro.Services.Limits.Models;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Limits
{
    public interface ILimitsService
    {
        Task<Limit> GetCurrentExchangeLimitsAsync();
    }
}
