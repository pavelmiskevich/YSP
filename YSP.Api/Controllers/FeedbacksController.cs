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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbacksController(IFeedbackService feedbackService, IMapper mapper)
        {
            _mapper = mapper;
            _feedbackService = feedbackService;
        }

        // GET: api/Feedbacks
        /// <summary>
        /// Action to get all feedbacks
        /// </summary>
        /// <returns>Returns the all feedbacks</returns>        
        /// <response code="200">Returned all feedbacks</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackResource>>> GetFeedbacks()
        {
            //TODO: проверить как возвращается пустой список
            var feedbacks = await _feedbackService.GetAllWithUser();
            var feedbackResources = _mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackResource>>(feedbacks);
            return Ok(feedbackResources);
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Action to get feedback by id
        /// </summary>
        /// <param name="id">Resource get feedback by id</param>
        /// <returns>Returns the found feedback</returns>        
        /// <response code="200">Returned if the feedback was found</response>
        /// <response code="404">Returned when the feedback is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackResource>> GetFeedbackById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            var feedbackResource = _mapper.Map<Feedback, FeedbackResource>(feedback);            

            return Ok(feedbackResource);
        }

        // GET: api/Feedbacks/User/5
        /// <summary>
        /// Action to get feedbacks by UserId
        /// </summary>
        /// <param name="id">Resource get feedbacks by user id</param>
        /// <returns>Returns the found feedbacks</returns>        
        /// <response code="200">Returned if the feedbacks was found</response>
        /// <response code="404">Returned when the feedbacks is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<FeedbackResource>>> GetFeedbackByUserId(int? userId)
        {
            //if (userId != 0)
            if (userId.HasValue)
            {
                var feedbacks = await _feedbackService.GetFeedbacksByUserId(userId.Value);
                var feedbackResource = _mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackResource>>(feedbacks);

                return Ok(feedbackResource);
            }
            else
                return await GetFeedbacks();
        }

        // POST: api/Feedbacks
        /// <summary>
        /// Action to insert new feedback
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new feedback</param>
        /// <returns>Returns the inserted new feedback</returns>        
        /// <response code="200">Returned if the feedback was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the feedback couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<FeedbackResource>> CreateFeedback([FromBody] SaveFeedbackResource saveFeedbackResource)
        {
            var validator = new SaveFeedbackResourceValidator();
            var validationResult = await validator.ValidateAsync(saveFeedbackResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var feedbackToCreate = _mapper.Map<SaveFeedbackResource, Feedback>(saveFeedbackResource);

            var newFeedback = await _feedbackService.CreateFeedback(feedbackToCreate);

            //var Feedback = await _FeedbackService.GetFeedbackById(newFeedback.Id);

            var feedbackResource = _mapper.Map<Feedback, FeedbackResource>(newFeedback); //Feedback

            return Ok(feedbackResource);
        }

        //TODO: сделать сохранение от имени пользователя
        // POST: api/Feedbacks/User/5
        [HttpPost("User/")]
        public async Task<ActionResult<FeedbackResource>> CreateFeedbackByUser([FromBody] SaveFeedbackResource saveFeedbackResource)
        {
            var validator = new SaveFeedbackResourceValidator();
            var validationResult = await validator.ValidateAsync(saveFeedbackResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var feedbackToCreate = _mapper.Map<SaveFeedbackResource, Feedback>(saveFeedbackResource);

            //TODO: доделать CreateFeedbackByUser
            var newFeedback = new Feedback();  //await _feedbackService.CreateFeedbackByUser(feedbackToCreate);

            //var Feedback = await _FeedbackService.GetFeedbackById(newFeedback.Id);

            var feedbackResource = _mapper.Map<Feedback, FeedbackResource>(newFeedback); //Feedback

            return Ok(feedbackResource);
        }

        // PUT: api/Feedbacks/5
        /// <summary>
        /// Action to update an existing feedback
        /// </summary>
        /// <param name="id">Id existing feedback</param>
        /// <param name="saveCategoryResource">Resource to update an existing feedback</param>
        /// <returns>Returns the updated feedback</returns>        
        /// <response code="200">Returned if the feedback was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the feedback couldn't be found</response>
        /// <response code="404">Returned when the feedback to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<ActionResult<FeedbackResource>> UpdateFeedback(int id, [FromBody] SaveFeedbackResource saveFeedbackResource)
        {
            var validator = new SaveFeedbackResourceValidator();
            var validationResult = await validator.ValidateAsync(saveFeedbackResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveFeedbackResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var feedbackToBeUpdate = await _feedbackService.GetFeedbackById(id);

            if (feedbackToBeUpdate == null)
                return NotFound();

            var feedback = _mapper.Map<SaveFeedbackResource, Feedback>(saveFeedbackResource);

            await _feedbackService.UpdateFeedback(feedbackToBeUpdate, feedback);

            var updatedFeedback = await _feedbackService.GetFeedbackById(id);
            var updatedFeedbackResource = _mapper.Map<Feedback, FeedbackResource>(updatedFeedback);

            return Ok(updatedFeedbackResource);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Action to delete an existing feedback by id
        /// </summary>
        /// <param name="id">Id existing feedback</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the feedback was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the feedback couldn't be found</response>
        /// <response code="404">Returned when the feedback to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            if (id == 0)
                return BadRequest();

            var feedback = await _feedbackService.GetFeedbackById(id);

            if (feedback == null)
                return NotFound();

            await _feedbackService.DeleteFeedback(feedback);

            return NoContent();
        }
    }
}
