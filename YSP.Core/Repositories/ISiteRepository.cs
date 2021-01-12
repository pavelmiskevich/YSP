using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface ISiteRepository : IRepository<Site>
    {
        Task<IEnumerable<Site>> GetAllWithCategoryAsync();
        Task<Site> GetWithCategoryByIdAsync(int id);
        Task<IEnumerable<Site>> GetAllWithCategoryByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Site>> GetAllWithRegionAsync();
        Task<Site> GetWithRegionByIdAsync(int id);
        Task<IEnumerable<Site>> GetAllWithRegionByRegionIdAsync(int regionId);
        Task<IEnumerable<Site>> GetAllWithUserAsync();
        Task<Site> GetWithUserByIdAsync(int id);
        Task<IEnumerable<Site>> GetAllWithUserByUserIdAsync(int userId);

        Task<IEnumerable<Site>> GetAllWithQueriesAsync();
        Task<Site> GetWithQueriesByIdAsync(int id);

        Task<IEnumerable<Site>> GetAllWithReferenceAsync();

        [Obsolete]
        Task<IEnumerable<Site>> GetAllWithSchedulesAsync();
        [Obsolete]
        Task<Site> GetWithSchedulesByIdAsync(int id);
    }
}
