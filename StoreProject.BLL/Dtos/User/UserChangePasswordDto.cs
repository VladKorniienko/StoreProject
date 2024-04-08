using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.User
{
    public class UserChangePasswordDto
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
