using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationBridge.Configuration.Core;
using Microsoft.Extensions.Logging;

namespace ConfigurationBridge.Web.Configuration
{
    public class SettingsValidator : ISettingsValidator
    {
        private readonly ILogger _logger;

        public SettingsValidator(ILogger<SettingsValidator> logger)
        {
            _logger = logger;
        }
        public bool TryValidate(IAppSettingsStructure settings, out AggregateException validationExceptions)
        {
            if (settings is null) throw new ArgumentNullException(nameof(settings));
            
            var exceptions = new List<Exception>();

            if (settings.ApplicationName is null) exceptions.Add(new ArgumentNullException(nameof(settings.ApplicationName)));
            if (string.IsNullOrWhiteSpace(settings.ApplicationName)) exceptions.Add(new ArgumentOutOfRangeException(nameof(settings.ApplicationName)));
            if (settings.Secrets is null) exceptions.Add(new ArgumentNullException(nameof(settings.ApplicationName)));            
            validationExceptions = new AggregateException(exceptions);

            _logger?.Log(LogLevel.Error, validationExceptions, $"The {nameof(settings)} failed validation. See aggregated exception for details.");

            return !exceptions.Any();

        }
    }
}
