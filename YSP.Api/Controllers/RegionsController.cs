using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YSP.Api.Resources;
using YSP.Api.Validators;
using YSP.Core.Models;
using YSP.Core.Services;
using YSP.Data;

namespace YSP.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _mapper;

        public RegionsController(IRegionService regionService, IMapper mapper)
        {
            _mapper = mapper;
            _regionService = regionService;
        }

        // GET: api/Regions
        /// <summary>
        /// Action to get all regions
        /// </summary>
        /// <returns>Returns the all regions</returns>        
        /// <response code="200">Returned all regions</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionResource>>> GetRegions()
        {
            var regions = await _regionService.GetAllRegions();
            var regionResources = _mapper.Map<IEnumerable<Region>, IEnumerable<RegionResource>>(regions);
            return Ok(regionResources);
        }

        // GET: api/Regions/5
        /// <summary>
        /// Action to get region by id
        /// </summary>
        /// <param name="id">Resource get region by id</param>
        /// <returns>Returns the found region</returns>        
        /// <response code="200">Returned if the region was found</response>
        /// <response code="404">Returned when the region is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionResource>> GetRegionById(int id)
        {
            var region = await _regionService.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionResource = _mapper.Map<Region, RegionResource>(region);            

            return Ok(regionResource);
        }

        // POST: api/Regions
        /// <summary>
        /// Action to insert new region
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new region</param>
        /// <returns>Returns the inserted new region</returns>        
        /// <response code="200">Returned if the region was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the region couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<RegionResource>> CreateRegion([FromBody] SaveRegionResource saveRegionResource)
        {
            var validator = new SaveRegionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveRegionResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var regionToCreate = _mapper.Map<SaveRegionResource, Region>(saveRegionResource);

            var newRegion = await _regionService.CreateRegion(regionToCreate);

            //var Region = await _RegionService.GetRegionById(newRegion.Id);

            var regionResource = _mapper.Map<Region, RegionResource>(newRegion); //Region

            return Ok(regionResource);
        }

        // PUT: api/Regions/5
        /// <summary>
        /// Action to update an existing region
        /// </summary>
        /// <param name="id">Id existing region</param>
        /// <param name="saveCategoryResource">Resource to update an existing region</param>
        /// <returns>Returns the updated region</returns>        
        /// <response code="200">Returned if the region was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the region couldn't be found</response>
        /// <response code="404">Returned when the region to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<RegionResource>> UpdateRegion(int id, [FromBody] SaveRegionResource saveRegionResource)
        {
            var validator = new SaveRegionResourceValidator();
            var validationResult = await validator.ValidateAsync(saveRegionResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveRegionResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var regionToBeUpdate = await _regionService.GetRegionById(id);

            if (regionToBeUpdate == null)
                return NotFound();

            var region = _mapper.Map<SaveRegionResource, Region>(saveRegionResource);

            await _regionService.UpdateRegion(regionToBeUpdate, region);

            var updatedRegion = await _regionService.GetRegionById(id);
            var updatedRegionResource = _mapper.Map<Region, RegionResource>(updatedRegion);

            return Ok(updatedRegionResource);
        }

        // DELETE: api/Regions/5
        /// <summary>
        /// Action to delete an existing region by id
        /// </summary>
        /// <param name="id">Id existing region</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the region was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the region couldn't be found</response>
        /// <response code="404">Returned when the region to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            if (id == 0)
                return BadRequest();

            var region = await _regionService.GetRegionById(id);

            if (region == null)
                return NotFound();

            await _regionService.DeleteRegion(region);

            return NoContent();
        }
    }
}
