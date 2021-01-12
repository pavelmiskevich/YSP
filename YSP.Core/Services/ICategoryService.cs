using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllWithParentCategory();
        Task<Category> GetCategoryById(int? id);
        Task<Category> GetCategoryWithParentById(int? id);
        Task<IEnumerable<Category>> GetCategoriesByParentCategoryId(int parentCategoryId);
        Task<Category> CreateCategory(Category newCategory);
        Task UpdateCategory(Category categoryToBeUpdated, Category category);
        Task DeleteCategory(Category category);
        Task<bool> CheckExists(int id);
    }
}
