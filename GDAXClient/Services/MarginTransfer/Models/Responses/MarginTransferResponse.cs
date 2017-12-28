using System;

namespace GDAXClient.Services.MarginTransfer.Models
{
    public class MarginTransferResponse
    {
        public DateTime created_at { get; set; }

        public Guid Id { get; set; }

        public string User_id { get; set; }

        public Guid Profile_id { get; set; }

        public Guid Margin_profile_id { get; set; }

        public string Type { get; set; }

        public int Amount { get; set; }

        public Currency Currency { get; set; }

        public Guid Account_id { get; set; }

        public Guid Margin_account_id { get; set; }

        public string Margin_product_id { get; set; }

        public string Status { get; set; }

        public int Nonce { get; set; }
    }
}
