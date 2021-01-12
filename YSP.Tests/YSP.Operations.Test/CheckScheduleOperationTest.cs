using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using YSP.Core;
using YSP.Core.Services;

namespace YSP.Operations.Test
{
    public class CheckScheduleOperationTest
    {
        private readonly CheckScheduleOperation _checkScheduleOperation;
        private readonly IUnitOfWork _unitOfWork;

        public CheckScheduleOperationTest()
        {
            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            this._checkScheduleOperation = serviceProvider.GetService<CheckScheduleOperation>();
        }

        [Fact]
        public async void ScheduleOperations_GetQueries4Check_ReturnQueries()
        {
            var actual = await _checkScheduleOperation.GetQueries4Check();
            var expected = 0;

            Assert.NotEqual(expected, actual.Count());
        }    
    }
}
