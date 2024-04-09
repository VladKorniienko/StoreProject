using StoreProject.BLL.Dtos.Token;
using StoreProject.BLL.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task ChangePassword(UserChangePasswordDto userWithNewPassword, string id);
        public Task<Response> Authenticate(UserLoginDto userLoginDto);
        public Task<UserDto> Register(UserRegisterDto userRegisterDto);
    }
}
