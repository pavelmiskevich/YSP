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

namespace YSP.RabbitMQ.PublicherWorkerService
{
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

                    RabbitMqConfiguration options = configuration.GetSection("RabbitMqConnection").Get<RabbitMqConfiguration>();

                    services.AddSingleton(configuration);
                    services.AddSingleton(options);

                    //services.AddTransient<ICustomerService, CustomerService>();
                    services.AddDbContext<YSPDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("YSP.Data")), ServiceLifetime.Singleton);

                    services
                        //.AddTransient<IPositionService, PositionService>()
                        .AddTransient<IScheduleService, ScheduleService>()
                        .AddTransient<IUserService, UserService>()
                        .AddTransient<IUnitOfWork, UnitOfWork>()
                        .AddTransient<CheckScheduleOperation>();

                    services.AddAutoMapper(typeof(MappingProfile));

                    services.AddHostedService<Worker>();
                });
    }
}
