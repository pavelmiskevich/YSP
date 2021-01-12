using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<IEnumerable<Region>> GetAllWithSitesAsync();
        Task<Region> GetWithSitesByIdAsync(int id);

        Task<Region> GetBySiteIdAsync(int siteId);
    }
}
