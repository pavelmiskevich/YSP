using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Core.Services;

namespace YSP.MVC.ViewComponents
{    
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllWithParentCategory();
            if (categories != null)
            {
                return View("CategoryPartialView", categories);
            }
            return new ContentViewComponentResult("Unable to locate records.");
        }
    }
}
