using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.Common.Constants;
using System.Security.Claims;

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

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfoWithRoleDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        //public async Task<ActionResult<IEnumerable<UserInfoWithRoleDto>>> GetUsers()
        //{
        //    var users = _userService.GetUsers();
        //    var userList = new List<UserInfoWithRoleDto>();
        //    await foreach (var user in users)
        //    {
        //        userList.Add(user);
        //    }
        //    return Ok(userList);
        //}

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            AuthenticationRequest request = new AuthenticationRequest
            {
                OldToken = Request.Cookies["token"]!,
                RefreshToken = Request.Cookies["refreshToken"]!
            };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != id && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }
            var user = await _userService.GetUser(id);
            return Ok(user);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(string id, UserUpdateDto user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }
            await _userService.UpdateUser(user, id);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }
        
        [Authorize]
        [HttpPut("{userId}/{productId}")]
        public async Task<IActionResult> BuyProduct(string userId, string productId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != userId && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }
            await _userService.BuyProduct(userId, productId);
            return NoContent();
        }
    }
}
