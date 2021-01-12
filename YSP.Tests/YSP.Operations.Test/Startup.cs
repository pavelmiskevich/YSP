using AutoMapper;
using Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YSP.Core;
using YSP.Core.Services;
using YSP.Data;
using YSP.Services;
using YSP.Services.Test;

namespace YSP.Operations.Test
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddLogging();
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services
            //    .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IPositionService, PositionService>()
            //    .AddTransient<IQueryService, QueryService>()
                .AddTransient<IScheduleService, ScheduleService>()
            //    .AddTransient<ISiteService, SiteService>()
            //    .AddTransient<ISystemStateService, SystemStateService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ScheduleOperations>()
                .AddTransient<CheckScheduleOperation>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddLogging(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("YSP.Collector.Program", LogLevel.Debug)
                        .AddFilter("YSP.Services", LogLevel.Debug)
                        .AddConsole();
                        //.AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt")));
                    //.AddProvider(ILoggerProvider);
                })
            .AddDbContext<YSPDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("YSP.Data")));
        }
    }
}
