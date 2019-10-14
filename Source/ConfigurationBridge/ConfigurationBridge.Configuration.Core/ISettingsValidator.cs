using System;

namespace ConfigurationBridge.Configuration.Core
{
    public interface ISettingsValidator
    {
        bool TryValidate(IAppSettingsStructure settings, out AggregateException validationExceptions);
    }
}