using System;
using System.Collections.Generic;
using ConfigurationBridge.Configuration.Core;
using ConfigurationBridge.Configuration.Crypto;

namespace ConfigurationBridge.Configuration.Intermediaries
{
    public abstract class SettingsDecryptorBase : ISettingsDecrypt
    {
        private readonly ICryptoAlgorithm _crypto;

        protected SettingsDecryptorBase(ICryptoAlgorithm crypto)
        {
            _crypto = crypto ?? throw new ArgumentNullException(nameof(crypto));
        }

        public string Decrypt(string key, IDictionary<string, string> keyValues)
        {
            var hashedKey = IsEncryptedKey(key) ? _crypto.HashKey(key) : key;

            if (!keyValues.ContainsKey(hashedKey)) throw new KeyNotFoundException();

            var value = keyValues[hashedKey];
            return IsEncryptedValue(value) ? DecryptValue(value) : value;

        }

        private string DecryptValue(string encodedString)
        {
            return _crypto.Decrypt(encodedString);                        
        }

        protected abstract bool IsEncryptedKey(string key);

        protected abstract bool IsEncryptedValue(string value);
    }
}