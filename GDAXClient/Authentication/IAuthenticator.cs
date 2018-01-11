namespace GDAXClient.Services.Accounts
{
    public interface IAuthenticator
    {
        string ApiKey { get; }

        string UnsignedSignature { get; }

        string Passphrase { get; }
    }
}
