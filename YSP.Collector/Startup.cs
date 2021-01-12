using AutoMapper;
using Logger;
using Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using YSP.Core;
using YSP.Core.Services;
using YSP.Data;
using YSP.Operations;
using YSP.Services;

namespace YSP.Collector
{
    //TODO: доделать тесты с Dependency Injection
    //https://github.com/pengweiqhca/Xunit.DependencyInjection
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
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IPositionService, PositionService>()
                .AddTransient<IQueryService, QueryService>()
                .AddTransient<IScheduleService, ScheduleService>()
                .AddTransient<ISiteService, SiteService>()
                .AddTransient<ISystemStateService, SystemStateService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ScheduleOperations>()
                //.AddSingleton<IMapper, Mapper>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddLogging(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("YSP.Collector.Program", LogLevel.Debug)
                        .AddFilter("YSP.Services", LogLevel.Debug)
                        .AddConsole()
                        .AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt")));
                    //.AddProvider(ILoggerProvider);
                })
                //.AddSingleton<ILogger, FileLogger>()
                .AddDbContext<YSPDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("YSP.Data")));
            //.BuildServiceProvider();

            #region appsettings.json            
            //var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");
            //var config = builder.Build();
            //string connectionString = config.GetConnectionString("DefaultConnection");
            //#endregion appsettings.json
            //#region InitialDB
            //var optionsBuilder = new DbContextOptionsBuilder<YSPDbContext>();
            //var options = optionsBuilder
            //    .UseSqlServer(connectionString)
            //    .Options;
            //using (var context = new YSPDbContext(options))
            //{
            //    //await Datalnitializer.RecreateDatabaseAsync();
            //    await Datalnitializer.ClearDataAsync();
            //    await Datalnitializer.InitializeDataAsync();

            //    foreach (var user in context.Users)
            //    {
            //        Console.WriteLine($"{user.Name} {user.Password}");
            //    }
            //}
            #endregion InitialDB            
            #region Logger
            //TODO: переделать на Log4Net https://habr.com/ru/post/310770/
            //using var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .AddFilter("Microsoft", LogLevel.Warning)
            //        .AddFilter("System", LogLevel.Warning)
            //        .AddFilter("YSP.Collector.Program", LogLevel.Debug)
            //        .AddFilter("YSP.Services", LogLevel.Debug)
            //        .AddConsole();
            //        //.AddProvider(ILoggerProvider);
            //});
            //loggerFactory.AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt")));
            ////loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt"));
            //ILogger logger = loggerFactory.CreateLogger<Program>();
            //logger.LogInformation("Example log message");
            //logger.LogDebug("Debug Example log message");
            //logger.LogWarning("Warning Example log message");
            //logger.LogCritical("Critical Example log message");
            //logger.LogError("Error Example log message");
            #endregion Logger
            #region DependencyInjection
            //https://sohabr.net/habr/post/421943/
            //var serviceProvider = new ServiceCollection()
            //    .AddLogging()
            //    .AddSingleton<ICategoryService, CategoryService>()
            //    .AddSingleton<IPositionService, PositionService>()
            //    .AddSingleton<IQueryService, QueryService>()
            //    .AddSingleton<IScheduleService, ScheduleService>()
            //    .AddSingleton<ISiteService, SiteService>()
            //    .AddSingleton<ISystemStateService, SystemStateService>()
            //    .AddSingleton<IUserService, UserService>()
            //    .AddSingleton<IUnitOfWork, UnitOfWork>()
            //    //.AddSingleton<IMapper, Mapper>()
            //    .AddAutoMapper(typeof(Program))
            //    .AddLogging(builder =>
            //    {
            //        builder
            //            .AddFilter("Microsoft", LogLevel.Warning)
            //            .AddFilter("System", LogLevel.Warning)
            //            .AddFilter("YSP.Collector.Program", LogLevel.Debug)
            //            .AddFilter("YSP.Services", LogLevel.Debug)
            //            .AddConsole()
            //            .AddProvider(new FileLoggerProvider(Path.Combine(Directory.GetCurrentDirectory(), "logs/", $"{DateTime.Now.ToString("yyyyMMdd")}logs.txt")));
            //        //.AddProvider(ILoggerProvider);
            //    })
            //    //.AddSingleton<ILogger, FileLogger>()
            //    .AddDbContext<YSPDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("YSP.Data")))
            //    .BuildServiceProvider();
            #endregion DependencyInjection            
            //var logger = serviceProvider.GetService<ILogger<Program>>();
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //}
    }
}
