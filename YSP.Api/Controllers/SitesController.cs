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
    public class SitesController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly IMapper _mapper;

        public SitesController(ISiteService siteService, IMapper mapper)
        {
            _mapper = mapper;
            _siteService = siteService;
        }

        // GET: api/Sites
        /// <summary>
        /// Action to get all sites
        /// </summary>
        /// <returns>Returns the all sites</returns>        
        /// <response code="200">Returned all sites</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteResource>>> GetSites()
        {
            //var sites = await _siteService.GetAllWithUser();
            var sites = await _siteService.GetAllWithReference();
            var siteResources = _mapper.Map<IEnumerable<Site>, IEnumerable<SiteResource>>(sites);
            return Ok(siteResources);
        }

        // GET: api/Sites/5
        /// <summary>
        /// Action to get site by id
        /// </summary>
        /// <param name="id">Resource get site by id</param>
        /// <returns>Returns the found site</returns>        
        /// <response code="200">Returned if the site was found</response>
        /// <response code="404">Returned when the site is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteResource>> GetSiteById(int id)
        {
            var site = await _siteService.GetSiteById(id);
            if (site == null)
            {
                return NotFound();
            }
            var siteResource = _mapper.Map<Site, SiteResource>(site);            

            return Ok(siteResource);
        }

        // POST: api/Sites
        /// <summary>
        /// Action to insert new site
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new site</param>
        /// <returns>Returns the inserted new site</returns>        
        /// <response code="200">Returned if the site was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the site couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<SiteResource>> CreateSite([FromBody] SaveSiteResource saveSiteResource)
        {
            var validator = new SaveSiteResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSiteResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var siteToCreate = _mapper.Map<SaveSiteResource, Site>(saveSiteResource);

            var newSite = await _siteService.CreateSite(siteToCreate);

            //var Site = await _SiteService.GetSiteById(newSite.Id);

            var siteResource = _mapper.Map<Site, SiteResource>(newSite); //Site

            return Ok(siteResource);
        }

        // PUT: api/Sites/5
        /// <summary>
        /// Action to update an existing site
        /// </summary>
        /// <param name="id">Id existing site</param>
        /// <param name="saveCategoryResource">Resource to update an existing site</param>
        /// <returns>Returns the updated site</returns>        
        /// <response code="200">Returned if the site was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the site couldn't be found</response>
        /// <response code="404">Returned when the site to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<SiteResource>> UpdateSite(int id, [FromBody] SaveSiteResource saveSiteResource)
        {
            var validator = new SaveSiteResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSiteResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveSiteResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var siteToBeUpdate = await _siteService.GetSiteById(id);

            if (siteToBeUpdate == null)
                return NotFound();

            var site = _mapper.Map<SaveSiteResource, Site>(saveSiteResource);

            await _siteService.UpdateSite(siteToBeUpdate, site);

            var updatedSite = await _siteService.GetSiteById(id);
            var updatedSiteResource = _mapper.Map<Site, SiteResource>(updatedSite);

            return Ok(updatedSiteResource);
        }

        // DELETE: api/Sites/5
        /// <summary>
        /// Action to delete an existing site by id
        /// </summary>
        /// <param name="id">Id existing site</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the site was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the site couldn't be found</response>
        /// <response code="404">Returned when the site to delete is not found</response>
        /// <response code="409">Returned when the site to delete have the child reference</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            if (id == 0)
                return BadRequest();

            var site = await _siteService.GetSiteById(id);

            if (site == null)
                return NotFound();

            try
            {
                await _siteService.DeleteSite(site);
            }
            catch (Exception ex)
            {
                return Conflict();
            }            

            return NoContent();
        }
    }
}
