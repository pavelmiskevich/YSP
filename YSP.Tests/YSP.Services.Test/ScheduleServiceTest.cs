using System;
using Xunit;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace YSP.Services.Test
{
    //https://docs.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-with-dotnet-test

    public class ScheduleServiceTest
    {
        private readonly IScheduleService _scheduleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ScheduleService> _logger;
        private readonly IMapper _mapper;
        //private readonly Mock<IMapper> _mapper;


        public ScheduleServiceTest()
        {
            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            #region appsettings.json            
            //var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");
            //var config = builder.Build();
            //string connectionString = config.GetConnectionString("DefaultConnection");
            #endregion appsettings.json
            #region InitialDB
            //var optionsBuilder = new DbContextOptionsBuilder<YSPDbContext>();
            //var options = optionsBuilder
            //    .UseSqlServer(connectionString)
            //    .Options;
            //var context = new YSPDbContext(options);
            ////using (var context = new YSPDbContext(options))
            ////{
            ////    //await Datalnitializer.RecreateDatabaseAsync(context);
            ////    await Datalnitializer.ClearDataAsync(context);
            ////    await Datalnitializer.InitializeDataAsync(context);

            ////    foreach (var user in context.Users)
            ////    {
            ////        Console.WriteLine($"{user.Name} {user.Password}");
            ////    }
            ////}
            #endregion InitialDB 

            //this._unitOfWork = new UnitOfWork(context);
            this._unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            this._mapper = serviceProvider.GetService<IMapper>(); // new Mock<IMapper>();
            this._logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<ScheduleService>(); //= serviceProvider.GetService<ILogger>();
            //mockContext.SetupGet(c => c.SomeClass).Returns(_mapper.Object);

            //this._scheduleService = new ScheduleService(_unitOfWork, _mapper, _logger);
            this._scheduleService = serviceProvider.GetService<IScheduleService>();
            //this._scheduleService = scheduleService;
        }

        [Fact]
        public async void ScheduleService_GetScheduleById_ReturnSchedule()
        {
            var actual = await _scheduleService.GetScheduleById(1);
            var expected = 1;

            Assert.Equal(expected, actual.Id);
            //Assert.False(result.Id, "1 should not be prime");
        }

        [Fact]
        public async void ScheduleService_GetCategoryById_ReturnNull()
        {
            var actual = await _scheduleService.GetScheduleById(-1);
            var expected = default(Schedule);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ScheduleService_GetAllWithQueryByDateAndCount_ReturnSchedules()
        {
            int count = 100;
            var actual = await _scheduleService.GetAllTodayWithQueryByCount(count);
            var expected = 0;

            Assert.NotEqual(expected, actual.Count());
        }

        [Fact]
        public async void ScheduleService_DeactivateScheduleById_NoReturn()
        {
            var actual = await _scheduleService.CreateSchedule(new Schedule { QueryId = 1 });
            var expected = true;
            Assert.Equal(expected, actual.IsActive);

            await _scheduleService.DeactivateScheduleById(actual.Id);
            actual = await _scheduleService.GetScheduleById(actual.Id);
            expected = false;

            Assert.Equal(expected, actual.IsActive);
        }
        
        [Fact]
        public async void ScheduleService_DeactivateScheduleAfterCheckById_NoReturn()
        {
            var actual = await _scheduleService.CreateSchedule(new Schedule { QueryId = 1 });
            var expected = true;
            Assert.Equal(expected, actual.IsActive);

            await _scheduleService.DeactivateScheduleAfterCheckById(actual.Id);
            actual = await _scheduleService.GetScheduleWithQueryById(actual.Id);
            expected = false;

            Assert.Equal(expected, actual.IsActive);
            Assert.Equal(DateTime.Now.Date, actual.Query.LastCheck?.Date);
        }
    }
}
