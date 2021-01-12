using System;
using System.IO;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using YSP.Data;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Linq;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace YSP.Services.Test
{
    //https://docs.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-with-dotnet-test

    public class PositionServiceTest
    {
        private readonly IPositionService _positionService;
        private readonly IQueryService _queryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PositionService> _logger;
        private readonly IMapper _mapper;
        //private readonly Mock<IMapper> _mapper;


        public PositionServiceTest()
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
                .CreateLogger<PositionService>(); //= serviceProvider.GetService<ILogger>();
            //mockContext.SetupGet(c => c.SomeClass).Returns(_mapper.Object);
            this._positionService = new PositionService(_unitOfWork); //, _mapper, _logger);
            this._queryService = new QueryService(_unitOfWork);            
        }

        [Fact]
        public async void PositionService_CreatePosition_ReturnPosition()
        {
            int pos = 5;
            var queries = await _queryService.GetAllWithSite();            
            int queryId = queries.First().Id;
            var expectedQueryId = queryId;
            var expectedPos = pos;
            var actual = await _positionService.CreatePosition(new Position { QueryId = queryId, Pos = pos });
                        
            Assert.Equal(expectedQueryId, actual.QueryId);
            Assert.Equal(expectedPos, actual.Pos);
                        
            var query = queries.Last();
            expectedQueryId = query.Id;
            expectedPos = pos = 15;

            actual = await _positionService.CreatePosition(new Position { Query = query, Pos = pos });            

            Assert.Equal(expectedQueryId, actual.QueryId);
            Assert.Equal(expectedPos, actual.Pos);
        }

        [Fact]
        public async void PositionService_CreateRangePosition_ReturnPositions()
        {
            var queries = await _queryService.GetAllWithSite();
            var positions = new List<Position>
            {
                new Position { Query = queries.OrderBy(r => Guid.NewGuid()).First(), Pos = new Random().Next(1, 99) },
                new Position { Query = queries.OrderBy(r => Guid.NewGuid()).First(), Pos = new Random().Next(1, 99) },
                new Position { Query = queries.OrderBy(r => Guid.NewGuid()).First(), Pos = new Random().Next(1, 99) },
                new Position { Query = queries.OrderBy(r => Guid.NewGuid()).First(), Pos = new Random().Next(1, 99) }
            };

            var actual = await _positionService.CreateRangePosition(positions);
            var expected = 4;

            Assert.Equal(expected, actual.Count());
        }

        [Fact]
        public async void ScheduleService_GetPositionById_Position()
        {
            var actual = await _positionService.GetPositionById(1);
            var expected = 1;

            Assert.Equal(expected, actual.Id);
            //Assert.False(result.Id, "1 should not be prime");
        }

        [Fact]
        public async void ScheduleService_GetPositionById_ReturnNull()
        {
            var actual = await _positionService.GetPositionById(-1);
            var expected = default(Position);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void ScheduleService_GetPositionsToday_ReturnPositions()
        {
            await _positionService.CreatePosition(new Position { QueryId = 1, Pos = 1 });
            var actual = await _positionService.GetPositionsToday();
            var expected = 0;

            Assert.NotEqual(expected, actual.Count());
        }

        [Fact]
        public async void ScheduleService_GetPositionsByDate_ReturnPositions()
        {
            await _positionService.CreatePosition(new Position { QueryId = 1, Pos = 1 });
            var actual = await _positionService.GetPositionsByDate(DateTime.Now.Date);
            var expected = 0;

            Assert.NotEqual(expected, actual.Count());
        }

        //TODO: доделать тесты
        //[Fact]
        //public async void ScheduleService_GetPositionsBySiteIdToday_ReturnPositions()
        //{

        //    var actual = await _positionService.GetPositionsBySiteIdToday(siteId);
        //    var expected = 0;

        //    Assert.NotEqual(expected, actual.Count());
        //}

        //[Fact]
        //public async void ScheduleService_GetPositionsBySiteIdAndDate_ReturnPositions()
        //{
        //    var siteId = _query.SiteId;

        //    var actual = await _positionService.GetPositionsBySiteIdAndDate(siteId, DateTime.Now.Date);
        //    var expected = 0;

        //    Assert.NotEqual(expected, actual.Count());
        //}
    }
}
