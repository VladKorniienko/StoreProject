using StoreProject.Common.Enums;

namespace StoreProject.BLL.Dtos.User
{
    public class UserRegisterDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
