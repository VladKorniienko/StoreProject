using StoreProject.BLL.Dtos.Product;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public decimal Balance { get; set; }
        public List<ProductPartialDto> Products { get; } = new();
    }
}
