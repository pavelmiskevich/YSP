using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IQueryService
    {
        Task<IEnumerable<Query>> GetAllWithSite();
        Task<Query> GetQueryById(int id);
        Task<IEnumerable<Query>> GetQueriesBySiteId(int siteId);
        Task<Query> CreateQuery(Query newQuery);
        Task UpdateQuery(Query queryToBeUpdated, Query query);
        Task DeleteQuery(Query query);
    }
}
