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
    public class QueriesController : ControllerBase
    {
        private readonly IQueryService _queryService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public QueriesController(IQueryService queryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _mapper = mapper;
            _queryService = queryService;
            _logger = logger;
        }

        // GET: api/Queries
        /// <summary>
        /// Action to get all queries
        /// </summary>
        /// <returns>Returns the all queries</returns>        
        /// <response code="200">Returned all queries</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueryResource>>> GetQueries()
        {
            var queries = await _queryService.GetAllWithSite();
            var queryResources = _mapper.Map<IEnumerable<Query>, IEnumerable<QueryResource>>(queries);
            return Ok(queryResources);
        }

        // GET: api/Queries/5
        /// <summary>
        /// Action to get query by id
        /// </summary>
        /// <param name="id">Resource get query by id</param>
        /// <returns>Returns the found query</returns>        
        /// <response code="200">Returned if the query was found</response>
        /// <response code="404">Returned when the query is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<QueryResource>> GetQueryById(int id)
        {
            var query = await _queryService.GetQueryById(id);
            if (query == null)
            {
                return NotFound();
            }
            var queryResource = _mapper.Map<Query, QueryResource>(query);            

            return Ok(queryResource);
        }

        // GET: api/Queries/Site/5
        /// <summary>
        /// Action to get queries by site id
        /// </summary>
        /// <param name="id">Resource get queries by site id</param>
        /// <returns>Returns the found queries</returns>        
        /// <response code="200">Returned if the queries was found</response>
        /// <response code="404">Returned when the queries is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Site/{siteId}")]
        public async Task<ActionResult<IEnumerable<QueryResource>>> GetQueryBySiteId(int siteId)
        {
            var queries = await _queryService.GetQueriesBySiteId(siteId);
            if (queries.Count() == 0)
            {
                return NotFound();
            }
            var queryResources = _mapper.Map<IEnumerable<Query>, IEnumerable<QueryResource>>(queries);
            _logger.LogInformation($"Total {queryResources.Count()} queries for site {siteId}");
            return Ok(queryResources);
        }

        // POST: api/Queries
        /// <summary>
        /// Action to insert new query
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new query</param>
        /// <returns>Returns the inserted new query</returns>        
        /// <response code="200">Returned if the query was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the query couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<QueryResource>> CreateQuery([FromBody] SaveQueryResource saveQueryResource)
        {
            var validator = new SaveQueryResourceValidator();
            var validationResult = await validator.ValidateAsync(saveQueryResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var queryToCreate = _mapper.Map<SaveQueryResource, Query>(saveQueryResource);

            var newQuery = await _queryService.CreateQuery(queryToCreate);

            //var Query = await _QueryService.GetQueryById(newQuery.Id);

            var queryResource = _mapper.Map<Query, QueryResource>(newQuery); //Query
            _logger.LogInformation($"CreateQuery successful");
            return Ok(queryResource);
        }

        // PUT: api/Queries/5
        /// <summary>
        /// Action to update an existing query
        /// </summary>
        /// <param name="id">Id existing query</param>
        /// <param name="saveCategoryResource">Resource to update an existing query</param>
        /// <returns>Returns the updated query</returns>        
        /// <response code="200">Returned if the query was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the query couldn't be found</response>
        /// <response code="404">Returned when the query to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<QueryResource>> UpdateQuery(int id, [FromBody] SaveQueryResource saveQueryResource)
        {
            var validator = new SaveQueryResourceValidator();
            var validationResult = await validator.ValidateAsync(saveQueryResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveQueryResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var queryToBeUpdate = await _queryService.GetQueryById(id);

            if (queryToBeUpdate == null)
                return NotFound();

            var query = _mapper.Map<SaveQueryResource, Query>(saveQueryResource);

            await _queryService.UpdateQuery(queryToBeUpdate, query);

            var updatedQuery = await _queryService.GetQueryById(id);
            var updatedQueryResource = _mapper.Map<Query, QueryResource>(updatedQuery);

            return Ok(updatedQueryResource);
        }

        // DELETE: api/Queries/5
        /// <summary>
        /// Action to delete an existing query by id
        /// </summary>
        /// <param name="id">Id existing query</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the query was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the query couldn't be found</response>
        /// <response code="404">Returned when the query to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuery(int id)
        {
            if (id == 0)
                return BadRequest();

            var query = await _queryService.GetQueryById(id);

            if (query == null)
                return NotFound();

            await _queryService.DeleteQuery(query);

            return NoContent();
        }
    }
}
