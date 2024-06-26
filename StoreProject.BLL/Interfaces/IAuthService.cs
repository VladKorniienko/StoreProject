﻿using StoreProject.BLL.Dtos.Token;
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
        public Task<UserInfoWithRoleDto> Register(UserRegisterDto userRegisterDto);
        public Task<AuthenticationResponse> RefreshToken(RefreshTokenRequest request);
        public Task<string> LogoutAsync(ClaimsPrincipal user);
        public Task AddToAdminRole(string id);
        public Task RemoveFromAdminRole(string id);
    }
}
