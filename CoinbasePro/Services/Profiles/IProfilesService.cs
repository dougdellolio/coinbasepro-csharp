using CoinbasePro.Services.Profiles.Models;
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
            string currency,
            decimal amount);
    }
}
