using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetAllRegions();
        Task<Region> GetRegionById(int id);
        Task<Region> CreateRegion(Region newRegion);
        Task UpdateRegion(Region regionToBeUpdated, Region region);
        Task DeleteRegion(Region region);
    }
}
