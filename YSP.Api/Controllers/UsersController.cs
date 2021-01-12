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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/Users
        /// <summary>
        /// Action to get all users
        /// </summary>
        /// <returns>Returns the all users</returns>        
        /// <response code="200">Returned all users</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return Ok(userResources);
        }

        // GET: api/Users/5
        /// <summary>
        /// Action to get user by id
        /// </summary>
        /// <param name="id">Resource get user by id</param>
        /// <returns>Returns the found user</returns>        
        /// <response code="200">Returned if the user was found</response>
        /// <response code="404">Returned when the user is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userResource = _mapper.Map<User, UserResource>(user);            

            return Ok(userResource);
        }

        // POST: api/Users
        /// <summary>
        /// Action to insert new user
        /// </summary>
        /// <param name="saveCategoryResource">Resource to insert new user</param>
        /// <returns>Returns the inserted new user</returns>        
        /// <response code="200">Returned if the user was inserted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the user couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<ActionResult<UserResource>> CreateUser([FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userToCreate = _mapper.Map<SaveUserResource, User>(saveUserResource);

            var newUser = await _userService.CreateUser(userToCreate);

            //var User = await _UserService.GetUserById(newUser.Id);

            var userResource = _mapper.Map<User, UserResource>(newUser); //User

            return Ok(userResource);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Action to update an existing user
        /// </summary>
        /// <param name="id">Id existing user</param>
        /// <param name="saveCategoryResource">Resource to update an existing user</param>
        /// <returns>Returns the updated user</returns>        
        /// <response code="200">Returned if the user was updated</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the user couldn't be found</response>
        /// <response code="404">Returned when the user to update is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]        
        public async Task<ActionResult<UserResource>> UpdateUser(int id, [FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            var validationResult = await validator.ValidateAsync(saveUserResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid; // || id != saveUserResource.Id

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var userToBeUpdate = await _userService.GetUserById(id);

            if (userToBeUpdate == null)
                return NotFound();

            var user = _mapper.Map<SaveUserResource, User>(saveUserResource);

            await _userService.UpdateUser(userToBeUpdate, user);

            var updatedUser = await _userService.GetUserById(id);
            var updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);

            return Ok(updatedUserResource);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Action to delete an existing user by id
        /// </summary>
        /// <param name="id">Id existing user</param>
        /// <returns>no returns</returns>        
        /// <response code="204">Returned if the user was deleted</response>
        /// <response code="400">Returned if the resource couldn't be parsed or the user couldn't be found</response>
        /// <response code="404">Returned when the user to delete is not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
                return BadRequest();

            var user = await _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            await _userService.DeleteUser(user);

            return NoContent();
        }
    }
}
