using StoreProject.BLL.Dtos.User;

namespace StoreProject.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsers();
        public Task<UserDto> GetUser(int id);
        public Task<UserDto> AddUser(UserLoginDto newUserDto);
        public Task UpdateUser(UserDto userToUpdate);
        public Task<bool> DeleteUser(int id);
        public Task BuyProduct(int userId, int productId);
    }
}
