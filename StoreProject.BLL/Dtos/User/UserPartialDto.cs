using StoreProject.Common.Enums;

namespace StoreProject.BLL.Dtos.User
{
    public class UserPartialDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public Role Role { get; set; }
        public decimal Balance { get; set; }
    }
}
