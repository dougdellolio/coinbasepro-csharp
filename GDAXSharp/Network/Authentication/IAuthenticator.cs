namespace GDAXSharp.Network.Authentication
{
    public interface IAuthenticator
    {
        string ApiKey { get; }

        string UnsignedSignature { get; }

        string Passphrase { get; }
    }
}
