using System;

namespace CoinbasePro.Services.Profiles.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
