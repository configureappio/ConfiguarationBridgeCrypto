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
            foreach (var lookupKey in new[] {key, _crypto.HashKey(key)})
            {
                if (keyValues.ContainsKey(lookupKey))
                {
                    var value = keyValues[lookupKey];
                    return IsEncryptedValue(value) ? DecryptValue(value) : value;
                }
            }

            throw new KeyNotFoundException();
        }

        private string DecryptValue(string encodedString)
        {
            return _crypto.Decrypt(encodedString);                        
        }

        
        protected abstract bool IsEncryptedValue(string value);
    }
}