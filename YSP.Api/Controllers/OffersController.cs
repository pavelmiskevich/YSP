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
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IMapper _mapper;

        public OffersController(IOfferService offerService, IMapper mapper)
        {
            _mapper = mapper;
            _offerService = offerService;
        }

        // GET: api/Offers
        /// <summary>
        /// Action to get all offers
        /// </summary>
        /// <returns>Returns the all offers</returns>        
        /// <response code="200">Returned all offers</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferResource>>> GetOffers()
        {
            //TODO: проверить как возвращается пустой список
            var offers = await _offerService.GetAllWithUser();
            var offerResources = _mapper.Map<IEnumerable<Offer>, IEnumerable<OfferResource>>(offers);
            return Ok(offerResources);
        }

        // GET: api/Offers/5
        /// <summary>
        /// Action to get offer by id
        /// </summary>
        /// <param name="id">Resource get offer by id</param>
        /// <returns>Returns the found offer</returns>        
        /// <response code="200">Returned if the offer was found</response>
        /// <response code="404">Returned when the offer is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OfferResource>> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferById(id);
            if (offer == null)
            {
                return NotFound();
            }
            var offerResource = _mapper.Map<Offer, OfferResource>(offer);            

            return Ok(offerResource);
        }

        // GET: api/Offers/User/5
        /// <summary>
        /// Action to get offers by UserId
        /// </summary>
        /// <param name="id">Resource get offers by user id</param>
        /// <returns>Returns the found offers</returns>        
        /// <response code="200">Returned if the offers was found</response>
        /// <response code="404">Returned when the offers is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<OfferResource>>> GetOfferByUserId(int? userId)
        {
            //if (userId != 0)
            if (userId.HasValue)
            {
                var offers = await _offerService.GetOffersByUserId(userId.Value);
                var offerResource = _mapper.Map<IEnumerable<Offer>, IEnumerable<OfferResource>>(offers);

                return Ok(offerResource);
            }
            else
                return await GetOffers();
        }

        // POST: api/Offers
        /// <summary>
        /// Action to insert new offer
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new offer</param>
        /// <returns>Returns the inserted new offer</returns>        
        /// <response code="200">Returned if the offer was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the offer couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<OfferResource>> CreateOffer([FromBody] SaveOfferResource saveOfferResource)
        {
            var validator = new SaveOfferResourceValidator();
            var validationResult = await validator.ValidateAsync(saveOfferResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var offerToCreate = _mapper.Map<SaveOfferResource, Offer>(saveOfferResource);

            var newOffer = await _offerService.CreateOffer(offerToCreate);

            //var Offer = await _OfferService.GetOfferById(newOffer.Id);

            var offerResource = _mapper.Map<Offer, OfferResource>(newOffer); //Offer

            return Ok(offerResource);
        }

        //TODO: сделать сохранение от имени пользователя
        // POST: api/Offers/User/5
        [HttpPost("User/")]
        public async Task<ActionResult<OfferResource>> CreateOfferByUser([FromBody] SaveOfferResource saveOfferResource)
        {
            var validator = new SaveOfferResourceValidator();
            var validationResult = await validator.ValidateAsync(saveOfferResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var offerToCreate = _mapper.Map<SaveOfferResource, Offer>(saveOfferResource);

            //TODO: доделать CreateOfferByUser
            var newOffer = new Offer();  //await _OfferService.CreateOfferByUser(OfferToCreate);

            //var Offer = await _OfferService.GetOfferById(newOffer.Id);

            var offerResource = _mapper.Map<Offer, OfferResource>(newOffer); //Offer

            return Ok(offerResource);
        }

        // PUT: api/Offers/5
        /// <summary>
        /// Action to update an existing offer
        /// </summary>
        /// <param name="id">Id existing offer</param>
        /// <param name="saveCategoryResource">Resource to update an existing offer</param>
        /// <returns>Returns the updated offer</returns>        
        /// <response code="200">Returned if the offer was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the offer couldn't be found</response>
        /// <response code="404">Returned when the offer to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<OfferResource>> UpdateOffer(int id, [FromBody] SaveOfferResource saveOfferResource)
        {
            var validator = new SaveOfferResourceValidator();
            var validationResult = await validator.ValidateAsync(saveOfferResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveOfferResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var offerToBeUpdate = await _offerService.GetOfferById(id);

            if (offerToBeUpdate == null)
                return NotFound();

            var offer = _mapper.Map<SaveOfferResource, Offer>(saveOfferResource);

            await _offerService.UpdateOffer(offerToBeUpdate, offer);

            var updatedOffer = await _offerService.GetOfferById(id);
            var updatedOfferResource = _mapper.Map<Offer, OfferResource>(updatedOffer);

            return Ok(updatedOfferResource);
        }

        // DELETE: api/Offers/5
        /// <summary>
        /// Action to delete an existing offer by id
        /// </summary>
        /// <param name="id">Id existing offer</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the offer was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the offer couldn't be found</response>
        /// <response code="404">Returned when the offer to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            if (id == 0)
                return BadRequest();

            var offer = await _offerService.GetOfferById(id);

            if (offer == null)
                return NotFound();

            await _offerService.DeleteOffer(offer);

            return NoContent();
        }
    }
}
