using StoreProject.BLL.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.User
{
    public class UserInfoWithRoleDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public decimal Balance { get; set; }
        public List<ProductPartialDto> Products { get; } = new();
    }
}
