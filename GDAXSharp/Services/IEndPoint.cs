using System.Net.Http;

namespace GDAXSharp.Services
{
    public interface IEndPoint
    {
        HttpMethod HttpMethod {get;}

        string Uri {get;}

        string Content { get; }
    }

    public class EndPoint : IEndPoint
    {
        public HttpMethod HttpMethod { get; }
        public string Uri { get; }
        public string Content { get; }

        public EndPoint(HttpMethod httpMethod,
            string uri, string content)
        {
            HttpMethod = httpMethod;
            Uri = uri;
            Content = content;
        }
    }
}
