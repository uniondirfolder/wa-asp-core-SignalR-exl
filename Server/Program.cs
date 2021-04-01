using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = GetSerilogLogger();
            try
            {
                Log.Information("Starting web host");
                var configuration = GetConfiguration();
                //CreateHostBuilder(args).Build().Run();
                var host = BuildWebHost(configuration, args);
                host.Run();
            }
            catch (Exception ex) 
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) 
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options =>
                {
                    var port = configuration.GetValue("ListenerPort", 10000);

                    options.Listen(IPAddress.Any, port, listenOptions =>
                    {
                        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                    });
                })
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseSerilog();

            return builder.Build();
        }
        private static IConfiguration GetConfiguration() 
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static Serilog.ILogger GetSerilogLogger() 
        {
            var configuration = new LoggerConfiguration()
                .WriteTo.File(
                    path: "Logs\\log-txt",
                    retainedFileCountLimit: 31,
                    shared: true,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz [{Level}] {Message} [{Properties}]{NewLine}{Exception}}")
                .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    IndexFormat = "test-server-index-{0:yyyy.MM}",
                    BufferBaseFilename = "Logs\\ElasticBuffer",
                    BufferLogShippingInterval = TimeSpan.FromSeconds(5),
                    BufferRetainedInvalidPayloadsLimitBytes = 5000,
                    BufferFileCountLimit = 31,
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug
                });
                    
            return configuration.CreateLogger();
        }
    }
}
