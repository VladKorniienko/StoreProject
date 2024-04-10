using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task ChangePassword(UserChangePasswordDto userWithNewPassword, string id);
        public Task<AuthenticationResponse> Authenticate(UserLoginDto userLoginDto);
        public Task<UserDto> Register(UserRegisterDto userRegisterDto);
        public Task<AuthenticationResponse> RefreshToken(AuthenticationRequest request);
        public Task<string> LogoutAsync(ClaimsPrincipal user);
    }
}
