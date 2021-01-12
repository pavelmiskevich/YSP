using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core.DTO;
using YSP.Core.Services;
using YSP.Search.Core.Classes.Static;

namespace YSP.Operations
{
    public class CheckScheduleOperation : BaseScheduleOperations
    {
        public CheckScheduleOperation(IScheduleService scheduleService, IUserService userService, ILogger<BaseScheduleOperations> logger) 
            : base(scheduleService, userService, logger)
        {

        }

        /// <summary>
        /// Get Queries in Schedule 4 Check Without CancellationToken
        /// </summary>
        /// <returns>Queries to check</returns>
        public async Task<IEnumerable<QueryRegionDTO>> GetQueries4Check()
        {
            return await GetQueries4Check(new CancellationToken());
        }

        /// <summary>
        /// Get Queries in Schedule 4 Check With CancellationToken
        /// </summary>
        /// <returns>Queries to check</returns>
        public async Task<IEnumerable<QueryRegionDTO>> GetQueries4Check(CancellationToken token)
        {
            IEnumerable<QueryRegionDTO> queryRegionDTOs = null;
            try
            {
                //обновить раписание на сегодня
                await _scheduleService.UpdateToday(token);
                //получить список запросов для обработки
                int allLimit = await _userService.GetAllYandexLimit();
                int count = LimitConstraint.GetLimit(allLimit);
                queryRegionDTOs = await _scheduleService.GetAllTodayWithQueryByCount(count); // GetAllTodayWithQuery();                
                //обновить расписание на завтра
                await _scheduleService.UpdateFuture(token);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error in Check(token) {ex.Message}");
                //return false;
            }
            return queryRegionDTOs;
            //return true;
        }
    }
}
