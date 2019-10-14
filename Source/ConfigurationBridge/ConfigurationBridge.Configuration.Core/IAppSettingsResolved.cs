namespace ConfigurationBridge.Configuration.Core
{
    public interface IAppSettingsResolved : IAppSettings, ISqlConnectionSettings, IOracleConnectionSettings
    {        
    }
}
