using System.Security.Cryptography;

namespace ConfigurationBridge.Configuration.Crypto
{
    internal interface ICryptoAlgorithm<T> : ICryptoAlgorithm where T : SymmetricAlgorithm, new()
    {
    }
}