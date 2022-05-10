using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace WiredBrain.Web
{
    public class Program
    {
        private static readonly Gauge _InfoGauge = Metrics.CreateGauge("web_info", "Web app info", "dotnet_version", "assembly_name", "app_version");

        public static void Main(string[] args)
        {
            _InfoGauge.Labels("3.1.7", "WiredBrain.Web", "0.1.0").Set(1);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
