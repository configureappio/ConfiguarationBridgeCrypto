using ConfigurationBridge.Configuration.Core;
using Microsoft.Extensions.Options;
using System;

namespace ConfigurationBridge.Configuration.Intermediaries
{
    public class MyAppSettingsBridge : IAppSettingsResolved
    {
        private readonly IOptions<MyAppSettings> _appSettings;

        private readonly ISettingsDecrypt _decryptor;

        public MyAppSettingsBridge(IOptionsSnapshot<MyAppSettings> appSettings, ISettingsDecrypt decryptor, ISettingsValidator validator) {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _decryptor = decryptor ?? throw new ArgumentException(nameof(decryptor));
            if (validator == null) throw new ArgumentNullException(nameof(validator));

            if (!validator.TryValidate(appSettings.Value, out var validationException))
            {
                throw validationException;
            }
        }

        public string ApplicationName => _appSettings.Value.ApplicationName;

        public string SqlConnectionSting => _decryptor.Decrypt("Sql", _appSettings.Value.Secrets);

        public string OracleConnectionSting => _decryptor.Decrypt("Oracle", _appSettings.Value.Secrets);
    }
}
