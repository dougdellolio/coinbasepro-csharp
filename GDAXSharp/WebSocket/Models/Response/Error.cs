namespace CoinbasePro.WebSocket.Models.Response
{
    public class Error : BaseMessage
    {
        public string Message { get; set; }

        public string Reason { get; set; }
    }
}
