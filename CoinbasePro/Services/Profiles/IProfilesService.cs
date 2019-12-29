using CoinbasePro.Services.Profiles.Models;
using CoinbasePro.Shared.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Profiles
{
    public interface IProfilesService
    {
        Task<IEnumerable<Profile>> GetAllProfilesAsync();

        Task<Profile> GetProfileByIdAsync(Guid id);

        Task<string> CreateProfileTransferAsync(
            Guid from,
            Guid to,
            Currency currency,
            decimal amount);
    }
}
