using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithParentAsync();
        Task<Category> GetWithParentByIdAsync(int? id);
        Task<IEnumerable<Category>> GetAllWithParentByParentIdAsync(int parentCategoryId);

        Task<IEnumerable<Category>> GetAllWithChildsAsync();
        Task<Category> GetWithChildsByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllWithSitesAsync();
        Task<Category> GetWithSitesByIdAsync(int id);
        Task<bool> CheckExistsAsync(int id);
    }
}
