using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace YSP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //https://stackoverflow.com/questions/53724202/asp-net-core-httpsredirectionmiddleware-failed-to-determine-the-https-port-for-r
                    //webBuilder.UseSetting("https_port", "5001");
                    //https://metanit.com/sharp/aspnet5/2.12.php
                    //config.AddJsonFile("settings/appsettings.Docker.json", optional: true);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
