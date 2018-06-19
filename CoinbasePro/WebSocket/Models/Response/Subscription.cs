using System.Collections.Generic;
using CoinbasePro.WebSocket.Models.Request;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Subscription : BaseMessage
    {
        public List<Channel> Channels { get; set; }
    }
}
