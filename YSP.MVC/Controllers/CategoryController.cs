using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YSP.Core.Models;
using YSP.Core.Services;
using YSP.Data;

namespace YSP.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly YSPDbContext _context;

        public CategoryController(ICategoryService categoryService, YSPDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllWithParentCategory();
            //return View(await _categoryService.GetAllWithParentCategory());
            return View("IndexWithViewComponent");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return BadRequest();
            //}

            //var category = await _context.Categories
            //    .Include(c => c.Parent)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var category = await _categoryService.GetCategoryWithParentById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentId")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Add(category);
                    //await _context.SaveChangesAsync();
                    await _categoryService.CreateCategory(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $@"Создать запись невозможно: {ex.Message}");
                    return View(category);
                }                
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //var category = await _context.Categories.FindAsync(id);
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ParentId,Id,TimeStamp,IsActive")] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryToBeUpdate = await _categoryService.GetCategoryById(id);

                    if (categoryToBeUpdate == null)
                        return NotFound();

                    await _categoryService.UpdateCategory(categoryToBeUpdate, category);
                }
                catch (DbUpdateConcurrencyException ex)
                { 
                    ModelState.AddModelError(string.Empty,
                    $@"Сохранить запись невозможно из-за ее обновления другим пользователем. {ex.Message}");
                    return View(category);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $@"Сохранить запись невозможно. {ex.Message}");
                    return View(category);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetCategoryWithParentById(id);

            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Categories, "Id", "Name", category.ParentId);
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id,Name,TimeStamp,IsActive")] Category сategory)
        {
            try
            {
                await _categoryService.DeleteCategory(сategory);
            }
            catch (DbUpdateConcurrencyException ex)
            { 
                ModelState.AddModelError(string.Empty, $@"Удалить запись невозможно из-за ее обновления другим пользователем. {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Удалить запись невозможно: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        //TODO: проверить изменение имени с DeleteConfirmed () на Delete ()
        //// POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }

        //    var category = await _categoryService.GetCategoryById(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
