using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.UserAccount.Models;

namespace CoinbasePro.Services.UserAccount
{
    public interface IUserAccountService
    {
        Task<IEnumerable<TrailingVolume>> GetTrailingVolumeAsync();
    }
}
