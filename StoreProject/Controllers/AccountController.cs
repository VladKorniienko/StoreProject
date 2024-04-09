using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;

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

        // PUT: api/Account/{id}/ChangePassword
        [HttpPut("{id}/changePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto userChangePasswordDto, string id)
        {
            await _authService.ChangePassword(userChangePasswordDto, id);
            return NoContent();
        }

        // POST: Account/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser(UserLoginDto user)
        {
            var authUserDto = await _authService.Authenticate(user);
            return Ok(authUserDto);
        }
        // POST: Account/register
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> RegisterUser(UserRegisterDto user)
        {
            var registerUserDto = await _authService.Register(user);
            return Created("", registerUserDto);
        }
        // POST: Account/register
        [HttpPost("refresh")]
        public async Task<ActionResult<UserRegisterDto>> RefreshToken(AuthenticationRequest request)
        {
            var newToken = await _authService.RefreshToken(request);
            return Created("", newToken);
        }
    }
}
