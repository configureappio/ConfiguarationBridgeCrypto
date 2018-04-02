namespace ConfigurationBridge.Configuration.Crypto
{
    public interface ICryptoAlgorithm
    {
        string Decrypt(string text);
        string Encrypt(string text);
        string HashKey(string text);
    }
}