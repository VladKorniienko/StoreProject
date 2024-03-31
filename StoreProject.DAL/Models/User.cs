using Microsoft.AspNetCore.Identity;
using StoreProject.Common.Enums;

namespace StoreProject.DAL.Models
{
    public class User : IdentityUser
    {
        public Role Role { get; set; } = Role.User;
        public decimal Balance { get; set; } = 0.00M;
        public List<Product> Products { get; } = new();
    }

}
