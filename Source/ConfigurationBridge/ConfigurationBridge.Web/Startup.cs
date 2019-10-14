using ConfigurationBridge.Configuration.Core;
using ConfigurationBridge.Configuration.Intermediaries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using ConfigurationBridge.Configuration.Crypto;
using ConfigurationBridge.Web.Configuration;

namespace ConfigurationBridge.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();            
            services.Configure<MyAppSettings>(Configuration.GetSection("MyAppSettings"));
            services.AddSingleton(Configuration);


            // In a real implementation, you would have a factory here to get the password and salt securely
            // from somewhere such as Azure Key Vault, Environmental variable / another json setting (obfuscated in some way)
            var salt = Configuration["CryptoSalt"];
            var pwd = Configuration["CryptoPwd"];

            services.AddSingleton( x => new CryptoFactory().Create<AesManaged>(pwd, salt));
            services.AddSingleton<ISettingsDecrypt, SettingsDecryptor>();
            services.AddSingleton<ISettingsValidator, SettingsValidator>();
            services.AddScoped<IAppSettingsResolved, MyAppSettingsBridge>();
            
            // add the other interfaces implemented by MyAppSettingsBridge to allow for resolution by those interfaces (interface segregation)
            services.AddScoped<IAppSettings>(provider => provider.GetService<IAppSettingsResolved>());
            services.AddScoped<ISqlConnectionSettings>(provider => provider.GetService<IAppSettingsResolved>());
            services.AddScoped<IOracleConnectionSettings>(provider => provider.GetService<IAppSettingsResolved>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {                        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
