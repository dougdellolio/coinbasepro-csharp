using System;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Match : BaseMessage
    {
        public long TradeId { get; set; }

        public Guid MakerOrderId { get; set; }

        public Guid TakerOrderId { get; set; }

        public string TakerUserId { get; set; }

        public string UserId { get; set; }

        public Guid TakerProfileId { get; set; }

        public Guid ProfileId { get; set; }

        public OrderSide Side { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public DateTimeOffset Time { get; set; }

        public string MakerUserId { get; set; }

        public Guid MakerProfileId { get; set; }

        public decimal MakerFeeRate { get; set; }

        public decimal TakerFeeRate { get; set; }
    }
}
