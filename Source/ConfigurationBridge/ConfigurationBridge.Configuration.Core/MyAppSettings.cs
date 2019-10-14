using System.Collections.Generic;

namespace ConfigurationBridge.Configuration.Core
{
    public class MyAppSettings : IAppSettingsStructure
    {
        public string ApplicationName { get; set; }
        public Dictionary<string, string> Secrets { get; set; } = new Dictionary<string, string>();
    } 
}
