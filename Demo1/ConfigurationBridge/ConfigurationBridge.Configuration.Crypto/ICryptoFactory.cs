using System;
using System.Security.Cryptography;

namespace ConfigurationBridge.Configuration.Crypto
{
    public interface ICryptoFactory
    {
        ICryptoAlgorithm Create<T>(string password, string salt) where T : SymmetricAlgorithm, new();
        ICryptoAlgorithm Create(Type type, string password, string salt);
    }
}