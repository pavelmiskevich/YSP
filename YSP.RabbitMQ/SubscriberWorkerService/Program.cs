using AutoMapper;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YSP.Core;
using YSP.Core.Services;
using YSP.Data;
using YSP.Operations;
using YSP.RabbitMQ.Options;
using YSP.Services;

namespace YSP.RabbitMQ.SubscriberWorkerService
{
    //https://devblogs.microsoft.com/premier-developer/demystifying-the-new-net-core-3-worker-service/
    //TODO: сделать запускаемую как службу
    //https://habr.com/ru/company/microsoft/blog/446512/
    //https://devblogs.microsoft.com/dotnet/net-core-and-systemd/
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
#if DEBUG
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
#endif
                    //TODO: добавить потокобезопасное логирование в файл
                    //logging.AddFile("..."); // <== Exception
                })
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    //https://stackoverflow.com/questions/58183920/how-to-setup-app-settings-in-a-net-core-3-worker-service
                    RabbitMqConfiguration options = configuration.GetSection("RabbitMqConnection").Get<RabbitMqConfiguration>();                    

                    services.AddSingleton(configuration);
                    services.AddSingleton(options);

                    //services.AddTransient<ICustomerService, CustomerService>();
                    services.AddDbContext<YSPDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("YSP.Data")), ServiceLifetime.Singleton);

                    services
                        .AddTransient<IPositionService, PositionService>()
                        .AddTransient<IScheduleService, ScheduleService>()
                        .AddTransient<IUserService, UserService>()
                        .AddTransient<IUnitOfWork, UnitOfWork>()
                        .AddTransient<ScheduleOperations>();

                    services.AddAutoMapper(typeof(MappingProfile));

                    services.AddHostedService<Worker>();
                });
    }
}
