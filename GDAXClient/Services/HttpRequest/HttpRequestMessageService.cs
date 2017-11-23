using GDAXClient.Authentication;
using GDAXClient.Utilities;
using GDAXClient.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Services.HttpRequest
{
    public class HttpRequestMessageService : IHttpRequestMessageService
    {
        private readonly IClock clock;

        public HttpRequestMessageService(IClock clock)
        {
            this.clock = clock;
        }

        private const string apiUri = "https://api.gdax.com";

        public HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, Authenticator authenticator, string requestUri, string contentBody = "")
        {
            var requestMessage = new HttpRequestMessage(httpMethod, new Uri(new Uri(apiUri), requestUri));
            var timeStamp = clock.GetTime().ToTimeStamp();
            var signedSignature = ComputeSignature(authenticator.UnsignedSignature, timeStamp, requestUri);

            AddHeaders(requestMessage, authenticator, signedSignature, timeStamp);
            return requestMessage;
        }

        private string ComputeSignature(string secret, double timestamp, string requestUri, string contentBody = "")
        {
            byte[] data = Convert.FromBase64String(secret);
            var prehash = timestamp + HttpMethod.Get.ToString().ToUpper() + requestUri + contentBody;
            return HashString(prehash, data);
        }

        private string HashString(string str, byte[] secret)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            using (var hmac = new HMACSHA256(secret))
            {
                byte[] hash = hmac.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void AddHeaders(HttpRequestMessage httpRequestMessage, Authenticator authenticator, string signedSignature, double timeStamp)
        {
            httpRequestMessage.Headers.Add("User-Agent", "GDax-Client");
            httpRequestMessage.Headers.Add("CB-ACCESS-KEY", authenticator.ApiKey);
            httpRequestMessage.Headers.Add("CB-ACCESS-TIMESTAMP", timeStamp.ToString());
            httpRequestMessage.Headers.Add("CB-ACCESS-SIGN", signedSignature);
            httpRequestMessage.Headers.Add("CB-ACCESS-PASSPHRASE", authenticator.Passphrase);
        }
    }
}

