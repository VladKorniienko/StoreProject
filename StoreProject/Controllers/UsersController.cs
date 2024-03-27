using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using StoreProject.DAL.Models;

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while retrieving users.");
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUser(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while retrieving the user with id " + id + ".");
            }
        }

        // POST:
        [HttpPost]
        public async Task<ActionResult<UserLoginDto>> PostUser(UserLoginDto newUser)
        {
            try
            {
                var createdUserDto = await _userService.AddUser(newUser);
                return Created("", createdUserDto);
            }
            catch (ArgumentException ex)
            {
                //logging
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while adding the user.");
            }
        }

        //PUT:
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                await _userService.UpdateUser(user);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while updating the user.");
            }
        }

        //DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while deleting the product.");
            }
        }
    }
}
