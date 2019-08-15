using CoinbasePro.Services.Fees.Models;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Fees
{
    public interface IFeesService
    {
        Task<Fee> GetCurrentFeesAsync();
    }
}
