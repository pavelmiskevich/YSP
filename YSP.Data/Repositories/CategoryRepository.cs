using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using YSP.Core.Models;
using YSP.Core.Repositories;

namespace YSP.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //TODO: тщательно проверить получение с родителями и детьми
        public CategoryRepository(YSPDbContext context)
            : base(context)
        { }

        private YSPDbContext YSPDbContext
        {
            get { return Context as YSPDbContext; }
        }

        public async Task<IEnumerable<Category>> GetAllWithParentAsync()
        {
            return await YSPDbContext.Categories
                .Include(i => i.Parent)
                .ToListAsync();
        }

        public async Task<Category> GetWithParentByIdAsync(int? id)
        {
            return await YSPDbContext.Categories
                .Include(i => i.Parent)
                .SingleOrDefaultAsync(c => c.Id == id); ;
        }

        public async Task<IEnumerable<Category>> GetAllWithParentByParentIdAsync(int parentCategoryId)
        {
            return await YSPDbContext.Categories
                .Include(i => i.Parent)
                .Where(c => c.ParentId == parentCategoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithChildsAsync()
        {
            return await YSPDbContext.Categories
                .Include(i => i.InverseParent)
                .ToListAsync();
        }

        public async Task<Category> GetWithChildsByIdAsync(int id)
        {
            return await YSPDbContext.Categories
                .Include(i => i.InverseParent)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllWithSitesAsync()
        {
            return await YSPDbContext.Categories
                .Include(i => i.Sites)
                .ToListAsync();
        }

        public async Task<Category> GetWithSitesByIdAsync(int id)
        {
            return await YSPDbContext.Categories
                .Include(i => i.Sites)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CheckExistsAsync(int id)
        {
            return await YSPDbContext.Categories
                .AnyAsync(c => c.Id == id);
        }
    }
}
