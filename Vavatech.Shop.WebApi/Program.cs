using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.Shop.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // dotnet add package Serilog.AspNetCore
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File(new CompactJsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Hour)
                // .WriteTo.Seq("http://localhost:5341")  // dotnet add package Serilog.Sinks.Seq
                // dotnet add package serilog.sinks.elasticsearch
                // .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                //{
                //    AutoRegisterTemplate = true,
                //    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
                //})
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   string environmentName = hostingContext.HostingEnvironment.EnvironmentName;

                   config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                   config.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
                   config.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
                   config.AddCommandLine(args);
                   config.AddEnvironmentVariables();
                   config.AddUserSecrets<Program>();
               })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
