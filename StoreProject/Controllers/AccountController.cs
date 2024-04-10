using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using StoreProject.Common.Constants;
using System.Security.Claims;

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser(UserLoginDto user)
        {
            var authUserDto = await _authService.Authenticate(user);
            return Ok(authUserDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> RegisterUser(UserRegisterDto user)
        {
            var registerUserDto = await _authService.Register(user);
            return Created("", registerUserDto);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<UserRegisterDto>> RefreshToken(AuthenticationRequest request)
        {
            var newToken = await _authService.RefreshToken(request);
            return Created("", newToken);
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();
            await _authService.ChangePassword(userChangePasswordDto, userId);
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}/makeAdmin")]
        public async Task<ActionResult> MakeAdmin(string id)
        {
            await _authService.AddToAdminRole(id);
            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}/revokeAdmin")]
        public async Task<ActionResult> RevokeAdmin(string id)
        {
            await _authService.RemoveFromAdminRole(id);
            return NoContent();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            var resultMessage = await _authService.LogoutAsync(User);
            return Ok(resultMessage);
        }

    }
}
