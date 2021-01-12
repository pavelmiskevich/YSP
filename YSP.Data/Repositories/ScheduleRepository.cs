using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Schedule>> GetAllWithQueryAsync()
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                .ToListAsync();
        }

        public async Task<Schedule> GetWithQueryByIdAsync(int id)
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                .SingleOrDefaultAsync(s => s.Id == id); ;
        }

        public async Task<IEnumerable<Schedule>> GetAllWithQueryByQueryIdAsync(int queryId)
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                .Where(s => s.QueryId == queryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllTodayWithQueryAsync()
        {
            return await GetAllWithQueryByDateAsync(DateTime.Now.Date);
        }

        public async Task<IEnumerable<Schedule>> GetAllWithQueryByDateAsync(DateTime date)
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                .Where(s => s.Date == date)
                .ToListAsync();
        }
        /// <summary>
        /// Расписание на сегодня с запросами, сайтами и регионами
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionAsync()
        {
            return await GetAllTodayWithQuerySiteRegionAsync(DateTime.Now.Date);
        }
        /// <summary>
        /// Расписание на дату с запросами, сайтами и регионами
        /// </summary>
        /// <param name="date">ограничительная дата</param>
        /// <returns></returns>
        public async Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionAsync(DateTime date)
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                    .ThenInclude(q => q.Site)
                            .ThenInclude(s => s.Region)
                .Where(s => s.Date == date && s.IsActive == true)
                .ToListAsync();
        }
        /// <summary>
        /// Schedule for today with sites ana regions by count
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionByCountAsync(int count)
        {
            return await GetAllTodayWithQuerySiteRegionByCountAsync(DateTime.Now.Date, count);
        }
        /// <summary>
        /// Schedule for date with sites ana regions by count
        /// </summary>
        /// <param name="date">ограничительная дата</param>
        /// <returns></returns>
        public async Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionByCountAsync(DateTime date, int count)
        {
            return await YSPDbContext.Schedules
                .Include(i => i.Query)
                    .ThenInclude(q => q.Site)
                            .ThenInclude(s => s.Region)
                .Where(s => s.Date == date && s.IsActive == true)
                .Take(count)
                .ToListAsync();
        }
        /// <summary>
        /// Get count scheldules for today
        /// </summary>
        /// <returns>count scheldules today</returns>
        public async ValueTask<int> GetCountTodayAsync()
        {
            return await GetCountByDateAsync(DateTime.Now.Date);
        }
        /// <summary>
        /// Get count scheldules by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>count scheldules on date</returns>
        public async ValueTask<int> GetCountByDateAsync(DateTime date)
        {
            return await YSPDbContext.Schedules
                .CountAsync(s => s.Date == date);
        }
        #region на будущее расширение
        //public async Task<IEnumerable<Schedule>> GetAllWithSiteAsync()
        //{
        //    return await YSPDbContext.Schedules
        //        .Include(i => i.Site)
        //        .ToListAsync();
        //}

        //public async Task<Schedule> GetWithSiteByIdAsync(int id)
        //{
        //    return await YSPDbContext.Schedules
        //        .Include(i => i.Site)
        //        .SingleOrDefaultAsync(s => s.Id == id); ;
        //}

        //public async Task<IEnumerable<Schedule>> GetAllWithSiteBySiteIdAsync(int siteId)
        //{
        //    return await YSPDbContext.Schedules
        //        .Include(i => i.Site)
        //        .Where(s => s.SiteId == siteId)
        //        .ToListAsync();
        //}
        #endregion на будущее расширение
    }
}
