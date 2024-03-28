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
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        // POST:
        [HttpPost]
        public async Task<ActionResult<UserLoginDto>> PostUser(UserLoginDto newUser)
        {
            var createdUserDto = await _userService.AddUser(newUser);
            return Created("", createdUserDto);
        }

        //PUT:
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, UserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _userService.UpdateUser(user);
            return NoContent();

        }

        //DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _userService.DeleteUser(id);
            return NoContent();

        }
    }
}
