using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using YSP.Core.Services;

namespace YSP.Operations
{
    public abstract class BaseScheduleOperations
    {
        protected readonly IScheduleService _scheduleService;
        protected readonly IUserService _userService;
        protected readonly ILogger<BaseScheduleOperations> _logger;

        //public BaseScheduleOperations()
        //{

        //}

        public BaseScheduleOperations(IScheduleService scheduleService, IUserService userService, ILogger<BaseScheduleOperations> logger)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _logger = logger;
        }
    }
}
