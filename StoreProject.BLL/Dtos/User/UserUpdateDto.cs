using StoreProject.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.User
{
    public class UserUpdateDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        public decimal Balance { get; set; }
    }
}
