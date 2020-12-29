using System;

namespace CoinbasePro.Services.Deposits.Models.Responses
{
    public class CryptoDepositAddressResponse
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string DestinationTag { get; set; }

        public AddressInfo AddressInfo { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Network { get; set; }

        public string Resource { get; set; }

        public string DepositUri { get; set; }

        public bool ExchangeDepositAddress { get; set; }
    }

    public class AddressInfo
    {
        public string Address { get; set; }

        public long DestinationTag { get; set; }
    }
}
