using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using YSP.Core;
using YSP.Core.DTO;
using YSP.Core.Services;

namespace YSP.Operations.Test
{
    public class ScheduleOperationsTest
    {
        private readonly ScheduleOperations _scheduleOperations;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly ILogger<ScheduleOperations> _logger;
        //private readonly IMapper _mapper;

        //private readonly IScheduleService _scheduleService;
        //private readonly IUserService _userService;
        //private readonly IPositionService _positionService;

        public ScheduleOperationsTest()
        {
            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //this._unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            ////this._mapper = serviceProvider.GetService<IMapper>();
            //this._logger = serviceProvider.GetService<ILoggerFactory>()
            //    .CreateLogger<ScheduleOperations>();

            //this._scheduleService = serviceProvider.GetService<IScheduleService>();
            //this._userService = serviceProvider.GetService<IUserService>();
            //this._positionService = serviceProvider.GetService<IPositionService>();

            //this._scheduleOperations = scheduleOperations;
            this._scheduleOperations = serviceProvider.GetService<ScheduleOperations>();
        }

        [Fact]
        public async void ScheduleOperations_Check_ReturnTrue()
        {
            var actual = await _scheduleOperations.Check();
            var expected = true;

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public async void ScheduleOperations_CheckSingle_ReturnTrue()
        {            
            var actual = await _scheduleOperations.CheckSingle(new QueryRegionDTO { QueryName = "Тестовый запрос"});
            var expected = true;

            Assert.NotEqual(expected, actual);
        }
    }
}
