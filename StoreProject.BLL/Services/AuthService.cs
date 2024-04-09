using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.Common.Exceptions;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        public AuthService(UserManager<User> userManager, IMapper mapper, IOptions<JwtSettings> jwtSettings) 
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        public async Task ChangePassword(UserChangePasswordDto userWithNewPassword, string id)
        {
            var existingUser = await CheckIfUserExists(id);
            var result = await _userManager.ChangePasswordAsync(existingUser, userWithNewPassword.OldPassword, userWithNewPassword.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
        }
        public async Task<Response> Authenticate(UserLoginDto userLoginDto)
        {
            var userToAuthenticate = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (userToAuthenticate == null)
            {
                throw new NotFoundException($"User with Email {userLoginDto.Email} not found.");
            }
            var result = await _userManager.CheckPasswordAsync(userToAuthenticate, userLoginDto.Password);
            if (!result)
            {
                throw new ArgumentException("Invalid password.");
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userToAuthenticate.UserName),
                new Claim(ClaimTypes.Email, userToAuthenticate.Email)
            };
            var roles = await _userManager.GetRolesAsync(userToAuthenticate);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenExpireTimeSeconds),
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            await _userManager.RemoveAuthenticationTokenAsync
                (userToAuthenticate, "REFRESHTOKENPROVIDER", "RefreshToken");
            var refreshToken = await _userManager.GenerateUserTokenAsync
                (userToAuthenticate, "REFRESHTOKENPROVIDER", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(userToAuthenticate, "REFRESHTOKENPROVIDER", "RefreshToken", refreshToken);
            return new Response()
            {
                Token = tokenString,
                RefreshToken = refreshToken
            };

        }
        public async Task<UserDto> Register(UserRegisterDto userRegisterDto)
        {
            //check wheter the user with the same email already exists in db
            //var existingUser = await _unitOfWork.Users.FindAsync(u => u.Email == newUserDto.Email);
            await CheckIfDuplicateEmailExists(userRegisterDto.Email);
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(userRegisterDto);

            //await _userManager.AddToRoleAsync(userToAuthenticate, Role.User.ToString());
            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }

            return _mapper.Map<UserDto>(newUser);
        }
        private async Task<User> CheckIfUserExists(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with the ID {id} doesn't exist.");
            }
            return user;
        }
        private async Task CheckIfDuplicateEmailExists(string email, string id = null)
        {
            var existingUser = await _userManager.Users.Where(u => u.Email == email && (id == null || u.Id != id)).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new ArgumentException($"User with the same email ({email}) already exists.");
            }
        }
    }
}
