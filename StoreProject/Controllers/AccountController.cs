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

            if (authUserDto != null)
            {
                // Set the JWT token in an HttpOnly cookie
                Response.Cookies.Append("token", authUserDto.Token,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(2),
                    HttpOnly = true,
                    Secure = true, // Only send the cookie over HTTPS.
                    SameSite = SameSiteMode.None // Helps mitigate CSRF attacks.
                });
                Response.Cookies.Append("refreshToken", authUserDto.RefreshToken, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
                return Ok(new { Id = authUserDto.Id });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> RegisterUser(UserRegisterDto user)
        {
            var registerUserDto = await _authService.Register(user);
            return Created("", registerUserDto);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            AuthenticationRequest request = new AuthenticationRequest
            {
                OldToken = Request.Cookies["token"]!,
                RefreshToken = Request.Cookies["refreshToken"]!
            };
            if (request.OldToken != null && request.RefreshToken != null)
            {
                var newToken = await _authService.RefreshToken(request);
                if (newToken != null)
                {
                    // Set the JWT token in an HttpOnly cookie
                    Response.Cookies.Append("token", newToken.Token, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(2),
                        HttpOnly = true,
                        Secure = true, // Only send the cookie over HTTPS.
                        SameSite = SameSiteMode.None // Helps mitigate CSRF attacks.
                    });
                    Response.Cookies.Append("refreshToken", newToken.RefreshToken, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                    return Ok(new { UserId = newToken.Id });
                }
            }
            return Unauthorized();
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
            // Clear the JWT token cookie
            Response.Cookies.Delete("jwt_token", new CookieOptions { Secure = true, HttpOnly = true });
            // Clear the refresh token cookie
            Response.Cookies.Delete("refresh_token", new CookieOptions { Secure = true, HttpOnly = true });
            return Ok(resultMessage);
        }

    }
}
