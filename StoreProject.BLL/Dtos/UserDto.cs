using StoreProject.DAL.Models;

namespace StoreProject.BLL.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public decimal Balance { get; set; }
        public List<Product> Products { get; } = new();
    }
}
