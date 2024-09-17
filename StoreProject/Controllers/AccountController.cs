using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly JwtSettings _jwtSettings;
        public AccountController(IAuthService authService, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult> AuthenticateUser(UserLoginDto user)
        {
            var authUserDto = await _authService.Authenticate(user);

            if (authUserDto != null)
            {
                Response.Cookies.Append("token", authUserDto.Token,
               new CookieOptions
               {
                   Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenExpireTimeSeconds),
                   HttpOnly = true,
                   Secure = true, // Only send the cookie over HTTPS.
                   SameSite = SameSiteMode.Lax // Helps mitigate CSRF attacks.
               });
                Response.Cookies.Append("refreshToken", authUserDto.RefreshToken, new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenExpireTimeSeconds),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
                return Ok(new { Id = authUserDto.Id, Role = authUserDto.Role});
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
            RefreshTokenRequest refreshTokenInfo = new RefreshTokenRequest
            {
                OldToken = Request.Cookies["token"]!,
                RefreshToken = Request.Cookies["refreshToken"]!
            };
            if (refreshTokenInfo.OldToken != null && refreshTokenInfo.RefreshToken != null)
            {
                var newToken = await _authService.RefreshToken(refreshTokenInfo);
                if (newToken != null)
                {
                    Response.Cookies.Append("token", newToken.Token, new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenExpireTimeSeconds),
                        HttpOnly = true,
                        Secure = true, // Only send the cookie over HTTPS.
                        SameSite = SameSiteMode.Lax // Helps mitigate CSRF attacks.
                    });
                    Response.Cookies.Append("refreshToken", newToken.RefreshToken, new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenExpireTimeSeconds),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax
                    });
                    return Ok(new { Id = newToken.Id, Role = newToken.Role});
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
        public async Task<ActionResult> Logout()
        {
            var resultMessage = await _authService.LogoutAsync(User);
            // Clear the JWT token cookie
            Response.Cookies.Delete("token", new CookieOptions { Secure = true, HttpOnly = true });
            // Clear the refresh token cookie
            Response.Cookies.Delete("refreshToken", new CookieOptions { Secure = true, HttpOnly = true });
            return Ok(resultMessage);
        }

    }
}
