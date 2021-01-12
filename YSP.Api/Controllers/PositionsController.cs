using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly ISiteService _siteService;
        private readonly IQueryService _queryService;

        public PositionsController(IPositionService positionService, IMapper mapper)
        {
            _mapper = mapper;
            _positionService = positionService;
        }

        // GET: api/Positions
        /// <summary>
        /// Action to get all positions by date
        /// </summary>
        /// <returns>Returns the all positions by date</returns>        
        /// <response code="200">Returned all positions by date</response>
        /// <response code="204">Returned if no positions</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PositionResource>>> GetPositionsByDate()
        {
            //TODO: проверить как возвращается пустой список            
            var positions = (List<Position>)await _positionService.GetPositionsToday();
            if (positions.Count() == 0)
                return NoContent();
            var positionResources = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionResource>>(positions);
            
            return Ok(positionResources);
        }

        // GET: api/Positions/date
        /// <summary>
        /// Action to get all positions by date
        /// </summary>
        /// <returns>Returns the all positions by date</returns>        
        /// <response code="200">Returned all positions by date</response>
        /// <response code="204">Returned if no positions</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<PositionResource>>> GetPositionsByDate(DateTime? date)
        {
            //TODO: проверить как возвращается пустой список
            var positions = (List<Position>)await _positionService.GetPositionsByDate((DateTime)date);
            if (positions.Count() == 0)
                return NoContent();
            var positionResources = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionResource>>(positions);
            
            return Ok(positionResources);
        }

        // GET: api/Positions/site/date
        /// <summary>
        /// Action to get all positions by site Id and date
        /// </summary>
        /// <returns>Returns the all positions by site Id and date</returns>        
        /// <response code="200">Returned all positions by site Id and date</response>
        /// <response code="400">Returned if the site id equal 0</response>
        /// <response code="404">Returned when the site is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{siteId}/{date}")]
        public async Task<ActionResult<IEnumerable<PositionResource>>> GetPositionsBySiteIdAndDate(int siteId, DateTime? date)
        {
            if (siteId == 0)
                return BadRequest();
            var site = await _siteService.GetSiteById(siteId);
            if (site == null)
                return NotFound();

            List<Position> positions = new List<Position>();
            if (date.HasValue)
                positions = (List<Position>)await _positionService.GetPositionsBySiteIdAndDate(siteId, (DateTime)date);
            else
                positions = (List<Position>)await _positionService.GetPositionsBySiteIdToday(siteId);            
            var positionResources = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionResource>>(positions);
            return Ok(positionResources);
        }

        // GET: api/Positions/query
        /// <summary>
        /// Action to get all positions by query Id and date
        /// </summary>
        /// <returns>Returns the all positions by query Id</returns>        
        /// <response code="200">Returned all positions by query Id</response>
        /// <response code="400">Returned if the query id equal 0</response>
        /// <response code="404">Returned when the query is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{queryId}")]
        public async Task<ActionResult<IEnumerable<PositionResource>>> GetPositionsByQueryId(int queryId)
        {
            if (queryId == 0)
                return BadRequest();
            var query = await _queryService.GetQueryById(queryId);
            if (query == null)
                return NotFound();

            //TODO: проверить как возвращается пустой список
            //TODO: сделать получение позиций по запросу ограничивая датами GetPositionsByQueryIdAndDate
            //List<Position> positions = new List<Position>();
            //if (date.HasValue)
            //    positions = await _positionService.GetPositionsByQueryIdAndDate(queryId, date);
            //else
            var positions = await _positionService.GetPositionsByQueryId(queryId);
            var positionResources = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionResource>>(positions);
            return Ok(positionResources);
        }
    }
}
