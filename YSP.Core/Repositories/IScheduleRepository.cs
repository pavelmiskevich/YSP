using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetAllWithQueryAsync();
        Task<Schedule> GetWithQueryByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetAllWithQueryByQueryIdAsync(int queryId);
        //Task<IEnumerable<Schedule>> GetAllWithSiteAsync();
        //Task<Schedule> GetWithSiteByIdAsync(int id);
        //Task<IEnumerable<Schedule>> GetAllWithSiteBySiteIdAsync(int siteId);
        Task<IEnumerable<Schedule>> GetAllTodayWithQueryAsync();
        Task<IEnumerable<Schedule>> GetAllWithQueryByDateAsync(DateTime date);
        Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionAsync();
        Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionAsync(DateTime date);
        Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionByCountAsync(int count);
        Task<IEnumerable<Schedule>> GetAllTodayWithQuerySiteRegionByCountAsync(DateTime date, int count);
        ValueTask<int> GetCountTodayAsync();
        ValueTask<int> GetCountByDateAsync(DateTime date);
    }
}
