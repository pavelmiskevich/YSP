using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface ISiteService
    {
        //TODO: понять нужен ли этот метод
        Task<IEnumerable<Site>> GetAllWithReference();
        Task<IEnumerable<Site>> GetAllWithCategory();
        Task<IEnumerable<Site>> GetAllWithRegion();
        Task<IEnumerable<Site>> GetAllWithUser();
        Task<Site> GetSiteById(int id);
        Task<IEnumerable<Site>> GetSitesByCategoryId(int categoryId);
        Task<IEnumerable<Site>> GetSitesByRegionId(int regionId);
        Task<IEnumerable<Site>> GetSitesByUserId(int userId);
        Task<Site> CreateSite(Site newSite);
        Task UpdateSite(Site siteToBeUpdated, Site site);
        Task DeleteSite(Site site);
    }
}
