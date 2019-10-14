using System.Collections.Generic;

namespace ConfigurationBridge.Configuration.Core
{
    public interface ISettingsDecrypt
    {
        string Decrypt(string key, IDictionary<string, string> keyValues);
    }
}