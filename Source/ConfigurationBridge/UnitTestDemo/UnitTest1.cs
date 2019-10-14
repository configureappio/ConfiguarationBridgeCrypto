using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ConfigurationBridge.Configuration.Core;
using ConfigurationBridge.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestDemo
{
    public class UnitTest1
    {
        [Fact]
        public void Ensure_Encrypted_Configuration_Overrides_Take_Effect()
        {
            var masterValue = "This is the Oracle Connection string from my unit test";
            var salt = "ThisIsNotTheProductionSalt";
            var password = "ThisIsNotTheProductionPassword";
            var crypto = new ConfigurationBridge.Configuration.Crypto.CryptoFactory();
            var alg = crypto.Create<AesManaged>(password, salt);
            var encryptedText = alg.Encrypt(masterValue);
            var args = new string[] {$"CryptoSalt={salt}", $"CryptoPwd={password}"};

            var overridingSqlMessage = new KeyValuePair<string, string>("MyAppSettings:Secrets:Sql", ".This is not encrypted");
            var overridingOracleMessage = new KeyValuePair<string, string>("MyAppSettings:Secrets:Oracle", encryptedText);

            var defaultBuilder = Program.CreateWebHostBuilder(args);
            var webHost = defaultBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddInMemoryCollection(new[] {overridingSqlMessage, overridingOracleMessage});
            }).Build();

            var sql = webHost.Services.GetRequiredService<ISqlConnectionSettings>();
            var oracle = webHost.Services.GetRequiredService<IOracleConnectionSettings>();

            Assert.Equal(overridingSqlMessage.Value, sql.SqlConnectionSting);
            Assert.Equal(masterValue, oracle.OracleConnectionSting);
        }
    }
}
