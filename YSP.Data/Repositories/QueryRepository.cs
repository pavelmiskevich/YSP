using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class QueryRepository : Repository<Query>, IQueryRepository
    {
        public QueryRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Query>> GetAllWithSiteAsync()
        {
            return await YSPDbContext.Queries
                .Include(i => i.Site)
                .ToListAsync();
        }

        public async Task<Query> GetWithSiteByIdAsync(int id)
        {
            return await YSPDbContext.Queries
                .Include(i => i.Site)
                .SingleOrDefaultAsync(q => q.Id == id); ;
        }

        public async Task<IEnumerable<Query>> GetAllWithSiteBySiteIdAsync(int siteId)
        {
            return await YSPDbContext.Queries
                .Include(i => i.Site)
                .Where(q => q.SiteId == siteId)
                .ToListAsync();
        }

        //TODO: не уверен, что нужен метод, который получит все запросы, да еще и с позициями
        public async Task<IEnumerable<Query>> GetAllWithPositionsAsync()
        {
            return await YSPDbContext.Queries
                .Include(i => i.Positions)
                .ToListAsync();
        }

        public async Task<Query> GetWithPositionsByIdAsync(int id)
        {
            return await YSPDbContext.Queries
                .Include(i => i.Positions)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        //TODO: не уверен, что нужен метод, который получит все запросы, да еще и с расписанием        
        public async Task<IEnumerable<Query>> GetAllWithSchedulesAsync()
        {
            return await YSPDbContext.Queries
                .Include(i => i.Schedules)
                .ToListAsync();
        }

        public async Task<Query> GetWithSchedulesByIdAsync(int id)
        {
            return await YSPDbContext.Queries
                .Include(i => i.Schedules)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        /// <summary>
        /// Все активные запросы
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetAllActiveAsync()
        {
            return await YSPDbContext.Queries
                .Where(x => x.IsActive == true && x.Site.IsActive == true)
                .ToListAsync();
        }
        /// <summary>
        /// Все активные запросы без раписания на сегодня
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetAllActiveWithoutScheduleTodayAsync()
        {
            return await GetAllActiveWithoutScheduleByDateAsync(DateTime.Now.Date);
        }
        /// <summary>
        /// Все активные запросы без раписания на дату
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetAllActiveWithoutScheduleByDateAsync(DateTime date)
        {
            return await YSPDbContext.Queries
                //.Where(x => x.IsActive == true && x.Site.IsActive == true)
                //.Where(x => x.IsActive == true && x.Site.IsActive == true && !YSPDbContext.Schedules.Any(qs => x.Id == qs.QueryId));                
                .Where(x => x.IsActive == true && x.Site.IsActive == true && !x.Schedules.Any(s => s.Date >= date))                
                .ToListAsync();
        }
        /// <summary>
        /// Заданное количество активных запросов без раписания на сегодня
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetActiveWithoutScheduleTodayByCountAsync(int count)
        {
            return await GetActiveWithoutScheduleByCountAndDateAsync(DateTime.Now.Date, count);
        }
        /// <summary>
        /// Заданное количество активных запросов без раписания на дату
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Query>> GetActiveWithoutScheduleByCountAndDateAsync(DateTime date, int count)
        {
            return await YSPDbContext.Queries
                //.Where(x => x.IsActive == true && x.Site.IsActive == true)
                //.Where(x => x.IsActive == true && x.Site.IsActive == true && !YSPDbContext.Schedules.Any(qs => x.Id == qs.QueryId));                
                .Where(x => x.IsActive == true && x.Site.IsActive == true && !x.Schedules.Any(s => s.Date >= date))
                .Take(count)
                .ToListAsync();
        }
    }
}
