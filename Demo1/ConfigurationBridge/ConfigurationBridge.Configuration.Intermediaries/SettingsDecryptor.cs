using System;
using System.Collections.Generic;
using ConfigurationBridge.Configuration.Core;
using ConfigurationBridge.Configuration.Crypto;

namespace ConfigurationBridge.Configuration.Intermediaries
{
    public class SettingsDecryptor : ISettingsDecrypt
    {
        private readonly ICryptoAlgorithm _crypto;
        public SettingsDecryptor(ICryptoAlgorithm crypto)
        {
            _crypto = crypto ?? throw new ArgumentNullException(nameof(crypto));
        }

        public string Decrypt(string key, IDictionary<string, string> keyValues)
        {
            var hashedKey = _crypto.HashKey(key);
            var value = keyValues[hashedKey];
            return DecryptValue(value);
        }

        private string DecryptValue(string encodedString)
        {
            return _crypto.Decrypt(encodedString);                        
        }
    }
}