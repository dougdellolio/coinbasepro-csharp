using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.CoinbaseAccounts.Models;

namespace CoinbasePro.Services.CoinbaseAccounts
{
    public interface ICoinbaseAccountsService
    {
        Task<IEnumerable<CoinbaseAccount>> GetAllAccountsAsync();
    }
}
