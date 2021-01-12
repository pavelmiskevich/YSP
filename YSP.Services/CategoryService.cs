using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Category> CreateCategory(Category newCategory)
        {
            await _unitOfWork.Categories.AddAsync(newCategory);
            await _unitOfWork.CommitAsync();
            return newCategory;
        }

        public async Task DeleteCategory(Category category)
        {
            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithParentCategory()
        {
            return await _unitOfWork.Categories
                .GetAllWithParentAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByParentCategoryId(int parentCategoryId)
        {
            return await _unitOfWork.Categories
                .GetAllWithParentByParentIdAsync(parentCategoryId);
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _unitOfWork.Categories
                .GetByIdAsync(id);
        }

        public async Task<Category> GetCategoryWithParentById(int? id)
        {
            return await _unitOfWork.Categories
                .GetWithParentByIdAsync(id);
        }

        public async Task UpdateCategory(Category categoryToBeUpdated, Category category)
        {
            categoryToBeUpdated.Name = category.Name;
            categoryToBeUpdated.ParentId = category.ParentId;
            categoryToBeUpdated.IsActive = category.IsActive;
            categoryToBeUpdated.TimeStamp = category.TimeStamp;

            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CheckExists(int id)
        {
            return await _unitOfWork.Categories
                .CheckExistsAsync(id);
        }
    }
}
