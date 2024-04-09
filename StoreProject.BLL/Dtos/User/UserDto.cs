using StoreProject.BLL.Dtos.Product;

namespace StoreProject.BLL.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public decimal Balance { get; set; }
        public List<ProductPartialDto> Products { get; } = new();
    }
}
