using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IQueryRepository : IRepository<Query>
    {
        Task<IEnumerable<Query>> GetAllWithSiteAsync();
        Task<Query> GetWithSiteByIdAsync(int id);
        Task<IEnumerable<Query>> GetAllWithSiteBySiteIdAsync(int siteId);

        Task<IEnumerable<Query>> GetAllWithPositionsAsync();
        Task<Query> GetWithPositionsByIdAsync(int id);
        Task<IEnumerable<Query>> GetAllWithSchedulesAsync();
        Task<Query> GetWithSchedulesByIdAsync(int id);

        Task<IEnumerable<Query>> GetAllActiveAsync();
        Task<IEnumerable<Query>> GetAllActiveWithoutScheduleTodayAsync();
        Task<IEnumerable<Query>> GetAllActiveWithoutScheduleByDateAsync(DateTime date);
        Task<IEnumerable<Query>> GetActiveWithoutScheduleTodayByCountAsync(int count);
        Task<IEnumerable<Query>> GetActiveWithoutScheduleByCountAndDateAsync(DateTime date, int count);
    }
}
