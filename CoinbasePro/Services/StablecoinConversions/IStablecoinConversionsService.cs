using System.Threading.Tasks;
using CoinbasePro.Services.StablecoinConversions.Models;

namespace CoinbasePro.Services.StablecoinConversions
{
    public interface IStablecoinConversionsService
    {
        Task<StablecoinConversionResponse> CreateConversion(
            string fromCurrency,
            string toCurrency,
            decimal amount);
    }
}
