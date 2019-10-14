using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace ConfigurationBridge.Web
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            BuildWebHost(CreateWebHostBuilder(args)).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(AddCustomConfiguration);

        private static IWebHost BuildWebHost(IWebHostBuilder builder) => builder.Build();

        private static void AddCustomConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(
                new PhysicalFileProvider(
                    @"C:\"),
                    @"secrets.json",
                    true,
                    true);
        }
    }
}
