using StoreProject.BLL.Dtos.User;

namespace StoreProject.BLL.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetUsers();
        public UserDto GetUser(int id);
        public bool AddUser(UserLoginDto newUserDto, out UserDto createdUserDto, out string error);
        public bool UpdateUser(UserDto userToUpdate, out string error);
        public bool DeleteUser(int id, out string error);
    }
}
