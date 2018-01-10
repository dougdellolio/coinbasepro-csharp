using GDAXClient.Services.Accounts;

namespace GDAXClient.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public Authenticator(
            string apiKey,
            string unsignedSignature,
            string passphrase,
            bool useTimeStampServer = false)
        {
            ApiKey = apiKey;
            UnsignedSignature = unsignedSignature;
            Passphrase = passphrase;
            UseTimeStampServer = useTimeStampServer;
        }

        public string ApiKey { get; }

        public string UnsignedSignature { get; }

        public string Passphrase { get; }

        public bool UseTimeStampServer { get; }

    }
}
