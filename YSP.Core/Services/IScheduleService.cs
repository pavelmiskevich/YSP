using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core.DTO;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllWithQuery();
        Task<Schedule> GetScheduleById(int id);
        Task<Schedule> GetScheduleWithQueryById(int id);
        Task<IEnumerable<Schedule>> GetSchedulesByQueryId(int queryId);
        Task<Schedule> CreateSchedule(Schedule newSchedule);
        Task UpdateSchedule(Schedule scheduleToBeUpdated, Schedule schedule);
        Task DeleteSchedule(Schedule schedule);

        //TODO: Site reference

        Task<IEnumerable<QueryRegionDTO>> GetAllTodayWithQuery();
        Task<IEnumerable<QueryRegionDTO>> GetAllWithQueryByDate(DateTime date);
        Task<IEnumerable<QueryRegionDTO>> GetAllTodayWithQueryByCount(int count);
        Task<IEnumerable<QueryRegionDTO>> GetAllWithQueryByDateAndCount(DateTime date, int count);
        ValueTask<int> GetCountToday();
        ValueTask<int> GetCountByDate(DateTime date);

        Task UpdateToday();
        Task UpdateToday(CancellationToken token);
        Task UpdateFuture();
        Task UpdateFuture(CancellationToken token);
        Task DeactivateScheduleById(int id);
        Task DeactivateScheduleAfterCheckById(int scheduleId);
    }
}
