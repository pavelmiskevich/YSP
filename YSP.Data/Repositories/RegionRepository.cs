using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        /// <summary>
        /// Все регионы с сайтами
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Region>> GetAllWithSitesAsync()
        {
            return await YSPDbContext.Regions
                .Include(i => i.Sites)
                .ToListAsync();
        }

        /// <summary>
        /// Регион по Id с сайтами
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public async Task<Region> GetWithSitesByIdAsync(int id)
        {
            return await YSPDbContext.Regions
                .Include(i => i.Sites)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Регион по Id сайта
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public async Task<Region> GetBySiteIdAsync(int siteId)
        {
            throw new System.NotImplementedException();

            return await YSPDbContext.Regions
                //.Include(i => i.Sites)
                .SingleOrDefaultAsync(r => r.Sites.FirstOrDefault().Id == siteId);
        }
    }
}
