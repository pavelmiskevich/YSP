using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YSP.Data;
using YSP.Data.Datalnitialization;

namespace YSP.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var webHost = CreateHostBuilder(args).Build().Run();
            var webHost = CreateHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                Datalnitializer.Context = services.GetRequiredService<YSPDbContext>();
                //#if DEBUG
                await Datalnitializer.RecreateDatabaseAsync();
                //await Datalnitializer.ClearDataAsync();
                await Datalnitializer.InitializeDataAsync();
                //#endif
            }

            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseWebRoot("static");
                });
    }
}
