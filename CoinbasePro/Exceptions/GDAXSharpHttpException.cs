using System;
using System.Net;
using System.Net.Http;
using CoinbasePro.Services;

namespace CoinbasePro.Exceptions
{
    public class GDAXSharpHttpException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; set; }

        public IEndPoint EndPoint { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }

        public GDAXSharpHttpException()
        {
        }

        public GDAXSharpHttpException(string message) : base(message)
        {
        }

        public GDAXSharpHttpException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
