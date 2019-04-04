using System.Threading.Tasks;
using CoinbasePro.Services.StablecoinConversions.Models;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.StablecoinConversions
{
    public interface IStablecoinConversionsService
    {
        Task<StablecoinConversionResponse> CreateConversion(
            Currency from,
            Currency to,
            decimal amount);
    }
}
