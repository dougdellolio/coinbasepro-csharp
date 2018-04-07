using GDAXSharp.WebSocket.Models.Request;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Subscription : BaseMessage
    {
        public List<Channel> Channels { get; set; }
    }
}
