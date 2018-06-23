using System;
using System.Net;
using System.Net.Http;
using CoinbasePro.Services;

namespace CoinbasePro.Exceptions
{
    public class CoinbaseProHttpException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; set; }

        public IEndPoint EndPoint { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }

        public CoinbaseProHttpException()
        {
        }

        public CoinbaseProHttpException(string message) : base(message)
        {
        }

        public CoinbaseProHttpException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
