using GDAXSharp.Network.Authentication;
using GDAXSharp.Shared.Utilities;
using GDAXSharp.Shared.Utilities.Clock;
using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace GDAXSharp.Network
{
    public class AbstractRequest : AbstractJson
    {
        protected readonly IAuthenticator Authenticator;

        protected readonly IClock Clock;

        protected readonly bool SandBox;

        public AbstractRequest(IAuthenticator authenticator, IClock clock, bool sandBox)
        {
            Authenticator = authenticator;
            Clock = clock;
            SandBox = sandBox;
        }

        protected static string ComputeSignature(HttpMethod httpMethod, string secret, double timestamp, string requestUri, string contentBody = "")
        {
            var convertedString = Convert.FromBase64String(secret);
            var prehash = timestamp.ToString("F0", CultureInfo.InvariantCulture) + httpMethod.ToString().ToUpper() + requestUri + contentBody;
            return HashString(prehash, convertedString);
        }

        private static string HashString(string str, byte[] secret)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using (var hmaccsha = new HMACSHA256(secret))
            {
                return Convert.ToBase64String(hmaccsha.ComputeHash(bytes));
            }
        }
    }
}
