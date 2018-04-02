using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace ConfigurationBridge.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var webhost = WebHost.CreateDefaultBuilder(args);

            // In the real world you could use this to map to a file outside of source control
            webhost.ConfigureAppConfiguration(c => { c.AddJsonFile(new PhysicalFileProvider(@"C:\"), @"mysecrets.json", true, true); });

            return webhost.UseStartup<Startup>().Build();
        }

        
    }
}
