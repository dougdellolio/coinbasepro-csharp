using System;

namespace GDAXClient.Services.Accounts
{
    public class CoinbaseAccount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        public bool Primary { get; set; }

        public bool Active { get; set; }

        public WireDepositInformation Wire_Deposit_Information { get; set; }

        public SepaDepositInformation Sepa_Deposit_Information { get; set; }
    }

    public class WireDepositInformation
    {
        public string Account_Number { get; set; }

        public string Routing_Number { get; set; }

        public string Bank_Name { get; set; }

        public string Bank_Address { get; set; }

        public BankCountry Bank_Country { get; set; }

        public string Account_Name { get; set; }

        public string Account_Address { get; set; }

        public string Reference { get; set; }
    }

    public class SepaDepositInformation
    {
        public string Iban { get; set; }

        public string Swift { get; set; }

        public string Bank_Name { get; set; }

        public string Bank_Address { get; set; }

        public string Bank_Country_Name { get; set; }

        public string Account_Name { get; set; }

        public string Account_Address { get; set; }

        public string Reference { get; set; }
    }

    public class BankCountry
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
