using Microsoft.AspNetCore.Identity;

namespace StoreProject.DAL.Models
{
    public class User : IdentityUser
    {
        public decimal Balance { get; set; } = 0.00M;
        public List<Product> Products { get; } = new();
    }
}
