using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using YSP.Api.Resources;
using YSP.Api.Validators;
using YSP.Core.Models;
using YSP.Core.Services;
using YSP.Data;

namespace YSP.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {            
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Categories
        /// <summary>
        /// Action to get all categories
        /// </summary>
        /// <returns>Returns the all categories</returns>        
        /// <response code="200">Returned all categories</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResource>>> GetCategories()
        {
            var categories = await _categoryService.GetAllWithParentCategory();
            var categoryResources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            _logger.LogInformation($"Total {categoryResources.Count()} categories");
            return Ok(categoryResources);
        }

        // GET: api/Categories/5
        /// <summary>
        /// Action to get category by id
        /// </summary>
        /// <param name="id">Resource get category by id</param>
        /// <returns>Returns the found category</returns>        
        /// <response code="200">Returned if the category was found</response>
        /// <response code="404">Returned when the category is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResource>> GetCategoryById(int id)
        {
            //if (id == null)
            //{
            //    return BadRequest();
            //}

            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            var categoryResource = _mapper.Map<Category, CategoryResource>(category);            

            return Ok(categoryResource);
        }

        // POST: api/Categories       
        /// <summary>
        /// Action to insert new category
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new category</param>
        /// <returns>Returns the inserted new category</returns>        
        /// <response code="200">Returned if the category was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the category couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<CategoryResource>> CreateCategory([FromBody] SaveCategoryResource saveCategoryResource)
        {
            var validator = new SaveCategoryResourceValidator();
            var validationResult = await validator.ValidateAsync(saveCategoryResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var categoryToCreate = _mapper.Map<SaveCategoryResource, Category>(saveCategoryResource);

            var newCategory = await _categoryService.CreateCategory(categoryToCreate);

            var category = await _categoryService.GetCategoryById(newCategory.Id);

            var categoryResource = _mapper.Map<Category, CategoryResource>(category); //newCategory

            return Ok(categoryResource);
        }



        // PUT: api/Categories/5
        /// <summary>
        /// Action to update an existing category
        /// </summary>
        /// <param name="id">Id existing category</param>
        /// <param name="saveCategoryResource">Resource to update an existing category</param>
        /// <returns>Returns the updated category</returns>        
        /// <response code="200">Returned if the category was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the category couldn't be found</response>
        /// <response code="404">Returned when the category to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<CategoryResource>> UpdateCategory(int id, [FromBody] SaveCategoryResource saveCategoryResource)
        {
            try
            {
                var validator = new SaveCategoryResourceValidator();
                var validationResult = await validator.ValidateAsync(saveCategoryResource);

                var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveCategoryResource.Id

                if (requestIsInvalid)
                    return BadRequest(validationResult.Errors);

                var categoryToBeUpdate = await _categoryService.GetCategoryById(id);

                if (categoryToBeUpdate == null)
                    return NotFound();

                var category = _mapper.Map<SaveCategoryResource, Category>(saveCategoryResource);

                await _categoryService.UpdateCategory(categoryToBeUpdate, category);

                var updatedCategory = await _categoryService.GetCategoryById(id);
                var updatedCategoryResource = _mapper.Map<Category, CategoryResource>(updatedCategory);

                return Ok(updatedCategoryResource);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //TODO: проверить работу структуры
                if (! await CategoryExists(id))
                {
                    _logger.LogError($"Error Categories not found with id {id} {ex.Message}");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error Categories was found with id {id} {ex.Message}");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Total Error {ex.Message}");
                return BadRequest(ex.Message);
            }            
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Action to delete an existing category by id
        /// </summary>
        /// <param name="id">Id existing category</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the category was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the category couldn't be found</response>
        /// <response code="404">Returned when the category to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == 0)
                return BadRequest();

            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            await _categoryService.DeleteCategory(category);

            return NoContent();
        }

        /// <summary>
        /// Check is exist Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> CategoryExists(int id)
        {
            return await _categoryService.CheckExists(id);
        }
    }
}
