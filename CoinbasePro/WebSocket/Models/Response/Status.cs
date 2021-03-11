namespace CoinbasePro.WebSocket.Models.Response
{
    public class Status : BaseMessage
    {
        public Product[] Products { get; set; }

        public Currency[] Currencies { get; set; }
    }

    public class Currency
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal MinSize { get; set; }

        public string Status { get; set; }

        public string StatusMessage { get; set; }

        public decimal MaxPrecision { get; set; }

        public string[] ConvertibleTo { get; set; }

        public Details Details { get; set; }
    }

    public class Details
    {
        public string Type { get; set; }

        public string Symbol { get; set; }

        public int NetworkConfirmations { get; set; }

        public int SortOrder { get; set; }

        public string CryptoAddressLink { get; set; }

        public string CryptoTransactionLink { get; set; }

        public string[] PushPaymentMethods { get; set; }

        public long? MaxWithdrawalAmount { get; set; }

        public string[] GroupTypes { get; set; }

        public string DisplayName { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }

        public string BaseCurrency { get; set; }

        public string QuoteCurrency { get; set; }

        public decimal BaseMinSize { get; set; }

        public decimal BaseMaxSize { get; set; }

        public decimal BaseIncrement { get; set; }

        public decimal QuoteIncrement { get; set; }

        public string DisplayName { get; set; }

        public string Status { get; set; }

        public string StatusMessage { get; set; }

        public decimal MinMarketFunds { get; set; }

        public decimal MaxMarketFunds { get; set; }

        public bool PostOnly { get; set; }

        public bool LimitOnly { get; set; }

        public bool CancelOnly { get; set; }
    }
}
