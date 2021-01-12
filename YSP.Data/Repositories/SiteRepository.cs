using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class SiteRepository : Repository<Site>, ISiteRepository
    {
        public SiteRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        //TODO: сделать метод для получения сайта по Id запроса

        #region with Category
        public async Task<IEnumerable<Site>> GetAllWithCategoryAsync()
        {
            return await YSPDbContext.Sites
                .Include(i => i.Category)
                .ToListAsync();
        }

        public async Task<Site> GetWithCategoryByIdAsync(int id)
        {
            return await YSPDbContext.Sites
                .Include(i => i.Category)
                .SingleOrDefaultAsync(s => s.Id == id); ;
        }

        public async Task<IEnumerable<Site>> GetAllWithCategoryByCategoryIdAsync(int categoryId)
        {
            return await YSPDbContext.Sites
                .Include(i => i.Category)
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }
        #endregion with Category
        #region with Region
        public async Task<IEnumerable<Site>> GetAllWithRegionAsync()
        {
            return await YSPDbContext.Sites
                .Include(i => i.Region)
                .ToListAsync();
        }

        public async Task<Site> GetWithRegionByIdAsync(int id)
        {
            return await YSPDbContext.Sites
                .Include(i => i.Region)
                .SingleOrDefaultAsync(s => s.Id == id); ;
        }

        public async Task<IEnumerable<Site>> GetAllWithRegionByRegionIdAsync(int regionId)
        {
            return await YSPDbContext.Sites
                .Include(i => i.Region)
                .Where(c => c.RegionId == regionId)
                .ToListAsync();
        }
        #endregion with Region
        #region with User
        public async Task<IEnumerable<Site>> GetAllWithUserAsync()
        {
            return await YSPDbContext.Sites
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<Site> GetWithUserByIdAsync(int id)
        {
            return await YSPDbContext.Sites
                .Include(i => i.User)
                .SingleOrDefaultAsync(s => s.Id == id); ;
        }

        public async Task<IEnumerable<Site>> GetAllWithUserByUserIdAsync(int userId)
        {
            return await YSPDbContext.Sites
                .Include(i => i.User)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        #endregion with User

        #region with Queries
        //TODO: сомневаюсь, что нужен метод для получения всех сайтов, да еще и с запросами
        public async Task<IEnumerable<Site>> GetAllWithQueriesAsync()
        {
            return await YSPDbContext.Sites
                .Include(i => i.Queries)
                .ToListAsync();
        }

        public async Task<Site> GetWithQueriesByIdAsync(int id)
        {
            return await YSPDbContext.Sites
                .Include(i => i.Queries)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        #endregion with Queries
        #region with Queries
        //TODO: сомневаюсь, что нужен метод для получения всех сайтов, да еще и с раписанием
        [Obsolete]
        public async Task<IEnumerable<Site>> GetAllWithSchedulesAsync()
        {
            return await YSPDbContext.Sites
                //.Include(i => i.Schedule)
                .ToListAsync();
        }
        [Obsolete]
        public async Task<Site> GetWithSchedulesByIdAsync(int id)
        {
            return await YSPDbContext.Sites
                //.Include(i => i.Schedule)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        #endregion with Queries

        #region with Reference
        public async Task<IEnumerable<Site>> GetAllWithReferenceAsync()
        {
            return await YSPDbContext.Sites
                .Include(i => i.Category)
                .Include(i => i.Region)
                .Include(i => i.User)
                .ToListAsync();
        }
        #endregion with Reference
    }
}
