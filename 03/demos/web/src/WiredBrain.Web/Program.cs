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
        private static readonly Gauge _InfoGauge = 
            Metrics.CreateGauge("app_info", "Application info", "dotnet_version", "assembly_name", "version");

        public static void Main(string[] args)
        {
            _InfoGauge.Labels("3.1.7", "WiredBrain.Web", "0.2.0").Set(1);
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
