using System;
using System.Threading.Tasks;
using CodeJam;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Ziv.CodeExample.Web
{
    [UsedImplicitly]
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        private static IServiceProvider ServiceProvider { get; set; }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        [UsedImplicitly]
        public static IWebHost BuildWebHost(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var webHost = WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables(prefix: "ENV_");
                })
                .ConfigureAppConfiguration(DisableAutoReload)
                .UseSerilog((provider, context, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithAspnetcoreHttpcontext(provider);
                })
                .UseConfiguration(Configuration)
                .UseStartup<Startup>()
                .Build();

            ServiceProvider = webHost.Services;
            return webHost;
        }

        private static void DisableAutoReload(WebHostBuilderContext _, IConfigurationBuilder config)
        {
            foreach (var item in config.Sources)
            {
                if (item is JsonConfigurationSource source)
                    source.ReloadOnChange = false;
            }
        }
    }
}