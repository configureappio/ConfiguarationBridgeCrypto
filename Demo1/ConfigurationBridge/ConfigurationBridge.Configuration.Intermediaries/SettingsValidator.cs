using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationBridge.Configuration.Core;

namespace ConfigurationBridge.Configuration.Intermediaries
{
    public class SettingsValidator : ISettingsValidator
    {
        public bool TryValidate(IAppSettingsStructure settings, out AggregateException validationExceptions)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            
            var exceptions = new List<Exception>();

            if (settings.ApplicationName == null) exceptions.Add(new ArgumentNullException(nameof(settings.ApplicationName)));
            if (string.IsNullOrWhiteSpace(settings.ApplicationName)) exceptions.Add(new ArgumentOutOfRangeException(nameof(settings.ApplicationName)));
            if (settings.Secrets == null) exceptions.Add(new ArgumentNullException(nameof(settings.ApplicationName)));            
            validationExceptions = new AggregateException(exceptions);
            return !exceptions.Any();

        }
    }
}
