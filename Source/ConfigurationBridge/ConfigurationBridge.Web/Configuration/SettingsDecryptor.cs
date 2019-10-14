using ConfigurationBridge.Configuration.Crypto;
using ConfigurationBridge.Configuration.Intermediaries;

namespace ConfigurationBridge.Web.Configuration
{
    public class SettingsDecryptor : SettingsDecryptorBase
    {
        public SettingsDecryptor(ICryptoAlgorithm crypto) : base(crypto)
        {
        }

        protected override bool IsEncryptedValue(string value) => !value.StartsWith(".");
    }
}