using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public partial class ScheduleService : IScheduleService
    {        
        /// <summary>
        /// Количество запросов в расписании на сегодня
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> GetCountToday()
        {
            return await GetCountByDate(DateTime.Now.Date);
        }
        /// <summary>
        /// Количество запросов в расписании на дату
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> GetCountByDate(DateTime date)
        {
            return await _unitOfWork.Schedules
                .GetCountByDateAsync(date);
        }

        /// <summary>
        /// Обновление расписания на сегодня
        /// </summary>
        /// <returns></returns>
        public async Task UpdateToday()
        {
            int myCount = await _unitOfWork.Users.GetAllYandexLimitAsync();
            int sCount = await GetCountToday();
            DateTime date = DateTime.Now.Date;
            List<Schedule> newSchedules = new List<Schedule>();
            if (sCount < myCount)
            {
                IEnumerable<Query> queries = await _unitOfWork.Queries.GetActiveWithoutScheduleTodayByCountAsync(myCount - sCount);
                foreach (var query in queries)
                {
                    newSchedules.Add(new Schedule { QueryId = query.Id });
                }
            }
            
            await _unitOfWork.Schedules.AddRangeAsync(newSchedules);
            await _unitOfWork.CommitAsync();
            //await UpdateFuture();
        }

        /// <summary>
        /// Обновление расписания на сегодня, с возможностью отмены
        /// </summary>
        /// <returns></returns>
        public async Task UpdateToday(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                _logger.LogWarning($"Операция прервана SchedulesUpdateCT");
                return;
            }
            _logger.LogWarning($"Операция не прервана SchedulesUpdateCT");
            await UpdateToday();
        }

        /// <summary>
        /// Обновление расписания на будущее
        /// </summary>
        /// <returns></returns>
        public async Task UpdateFuture()
        {
            DateTime date = DateTime.Now.Date.AddDays(1);
            int myCount = await _unitOfWork.Users.GetAllYandexLimitAsync();
            int sCount = await GetCountByDate(date);            
            List<Schedule> newSchedules = new List<Schedule>();
            if (sCount < myCount)
            {
                int i = 1;
                IEnumerable<Query> queries = await _unitOfWork.Queries.GetActiveWithoutScheduleByCountAndDateAsync(date, myCount - sCount);
                foreach (var query in queries)
                {
                    if (newSchedules.Count == myCount * i)
                    {
                        i++;
                        date = date.AddDays(1);
                    }
                    newSchedules.Add(new Schedule { QueryId = query.Id, Date = date });
                }
            }
            //else
            //{
            //    date = date.AddDays(1);
            //    //sCount = await GetCountByDate(date);
            //    int i = 1;
            //    IEnumerable<Query> queries = await _unitOfWork.Queries.GetAllActiveWithoutScheduleByDateAsync(date);                
            //    foreach (var query in queries)
            //    {
            //        if (newSchedules.Count == myCount * i)
            //        {
            //            i++;
            //            date = date.AddDays(1);
            //        }
            //        newSchedules.Add(new Schedule { QueryId = query.Id, Date = date });
            //    }
            //}
            
            await _unitOfWork.Schedules.AddRangeAsync(newSchedules);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Обновление расписания на будущее, с возможностью отмены
        /// </summary>
        /// <returns></returns>
        public async Task UpdateFuture(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                _logger.LogWarning($"Операция прервана SchedulesUpdateFutureCT");
                return;
            }
            _logger.LogWarning($"Операция не прервана SchedulesUpdateFutureCT");
            await UpdateFuture();
        }

        /// <summary>
        /// Деактивировать респисание
        /// </summary>
        /// <param name="id">schedule id</param>
        /// <returns>no data</returns>
        public async Task DeactivateScheduleById(int id)
        {
            var schedule = await _unitOfWork.Schedules.GetByIdAsync(id);
            schedule.IsActive = false;
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Деактивировать проверенное респисание
        /// </summary>
        /// <param name="id">schedule id</param>
        /// <returns>no data</returns>
        public async Task DeactivateScheduleAfterCheckById(int scheduleId) //, int queryId
        {
            //var schedule = await _unitOfWork.Schedules.GetByIdAsync(scheduleId);
            var schedule = await _unitOfWork.Schedules.GetWithQueryByIdAsync(scheduleId);
            schedule.IsActive = false;
            schedule.Query.LastCheck = DateTime.Now;
            //var query = await _unitOfWork.Queries.GetByIdAsync(queryId);
            //query.LastCheck = DateTime.Now;
            await _unitOfWork.CommitAsync();
        }
    }
}
