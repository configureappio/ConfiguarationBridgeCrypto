using System;
using ConfigurationBridge.Configuration.Core;
using Microsoft.Extensions.Options;

namespace ConfigurationBridge.Web.Configuration
{
    public class MyAppSettingsBridge : IAppSettingsResolved
    {
        public MyAppSettingsBridge(IOptionsMonitor<MyAppSettings> appSettings, ISettingsDecrypt decryptor, ISettingsValidator validator)
        {

            var appSettingsUnwrapped = appSettings?.CurrentValue ?? throw new ArgumentNullException(nameof(appSettings));

            if (decryptor is null) throw new ArgumentException(nameof(decryptor));

            if (validator is null) throw new ArgumentNullException(nameof(validator));

            if (!validator.TryValidate(appSettingsUnwrapped, out var validationException))
            {
                throw validationException;
            }

            ApplicationName = appSettingsUnwrapped.ApplicationName;

            SqlConnectionSting = decryptor.Decrypt("Sql", appSettingsUnwrapped.Secrets);

            OracleConnectionSting = decryptor.Decrypt("Oracle", appSettingsUnwrapped.Secrets);

        }

        public string ApplicationName { get; }
    
        public string SqlConnectionSting { get; }

        public string OracleConnectionSting { get; }
    }
}
