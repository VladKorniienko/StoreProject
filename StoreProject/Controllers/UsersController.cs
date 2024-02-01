using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos;
using StoreProject.BLL.Interfaces;

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
            var users = _userService.GetUsers();
            return Ok(users);
        }

        // POST:
        [HttpPost]
        public async Task<ActionResult<UserLoginDto>> PostUser(UserLoginDto newUser)
        {
            if (!_userService.AddUser(newUser, out UserDto createdUserDto, out string error))
                return BadRequest(error);
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
            if (!_userService.UpdateUser(user, out string error))
                return NotFound(error);
            return NoContent();
        }

        //DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!_userService.DeleteUser(id, out string error))
            {
                return NotFound(error);
            }
            return NoContent();
        }


    }
}
