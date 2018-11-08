using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.IO;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace NetCoreWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .UseUrls("http://*:8686;http://*:8687")
             .UseKestrel(o=>
             {
                 o.Limits.MaxRequestBodySize = 10 * 1024;
                 o.Limits.MinRequestBodyDataRate =
                     new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                 o.Limits.MinResponseDataRate =
                     new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
             })
             .UseIISIntegration()
             .UseContentRoot(Directory.GetCurrentDirectory())
             .ConfigureServices(services => services.AddAutofac())
             .UseStartup<Startup>();
    }
}
