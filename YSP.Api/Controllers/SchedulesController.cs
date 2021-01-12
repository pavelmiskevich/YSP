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
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _mapper = mapper;
            _scheduleService = scheduleService;
        }

        // GET: api/Schedules
        /// <summary>
        /// Action to get all schedules
        /// </summary>
        /// <returns>Returns the all schedules</returns>        
        /// <response code="200">Returned all schedules</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleResource>>> GetSchedules()
        {
            //TODO: предусмотреть возможность изменения даты
            var schedules = await _scheduleService.GetAllWithQuery();
            var scheduleResources = _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(schedules);
            return Ok(scheduleResources);
        }

        // GET: api/Schedules/5
        /// <summary>
        /// Action to get schedule by id
        /// </summary>
        /// <param name="id">Resource get schedule by id</param>
        /// <returns>Returns the found schedule</returns>        
        /// <response code="200">Returned if the schedule was found</response>
        /// <response code="404">Returned when the schedule is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleResource>> GetScheduleById(int id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(schedule);            

            return Ok(scheduleResource);
        }

        // POST: api/Schedules
        /// <summary>
        /// Action to insert new schedule
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new schedule</param>
        /// <returns>Returns the inserted new schedule</returns>        
        /// <response code="200">Returned if the schedule was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the schedule couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<ScheduleResource>> CreateSchedule([FromBody] SaveScheduleResource saveScheduleResource)
        {
            var validator = new SaveScheduleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveScheduleResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var scheduleToCreate = _mapper.Map<SaveScheduleResource, Schedule>(saveScheduleResource);

            var newSchedule = await _scheduleService.CreateSchedule(scheduleToCreate);

            //var Schedule = await _ScheduleService.GetScheduleById(newSchedule.Id);

            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(newSchedule); //Schedule

            return Ok(scheduleResource);
        }

        // PUT: api/Schedules/5
        /// <summary>
        /// Action to update an existing schedule
        /// </summary>
        /// <param name="id">Id existing schedule</param>
        /// <param name="saveCategoryResource">Resource to update an existing schedule</param>
        /// <returns>Returns the updated schedule</returns>        
        /// <response code="200">Returned if the schedule was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the schedule couldn't be found</response>
        /// <response code="404">Returned when the schedule to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<ScheduleResource>> UpdateSchedule(int id, [FromBody] SaveScheduleResource saveScheduleResource)
        {
            var validator = new SaveScheduleResourceValidator();
            var validationResult = await validator.ValidateAsync(saveScheduleResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveScheduleResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var scheduleToBeUpdate = await _scheduleService.GetScheduleById(id);

            if (scheduleToBeUpdate == null)
                return NotFound();

            var schedule = _mapper.Map<SaveScheduleResource, Schedule>(saveScheduleResource);

            await _scheduleService.UpdateSchedule(scheduleToBeUpdate, schedule);

            var updatedSchedule = await _scheduleService.GetScheduleById(id);
            var updatedScheduleResource = _mapper.Map<Schedule, ScheduleResource>(updatedSchedule);

            return Ok(updatedScheduleResource);
        }

        // DELETE: api/Schedules/5
        /// <summary>
        /// Action to delete an existing schedule by id
        /// </summary>
        /// <param name="id">Id existing schedule</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the schedule was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the schedule couldn't be found</response>
        /// <response code="404">Returned when the schedule to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (id == 0)
                return BadRequest();

            var schedule = await _scheduleService.GetScheduleById(id);

            if (schedule == null)
                return NotFound();

            await _scheduleService.DeleteSchedule(schedule);

            return NoContent();
        }
    }
}
