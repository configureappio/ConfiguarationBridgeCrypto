using System.Collections.Generic;

namespace ConfigurationBridge.Configuration.Core
{
    public interface IAppSettingsStructure : IAppSettings
    {
        Dictionary<string, string> Secrets { get; }
    }
}
