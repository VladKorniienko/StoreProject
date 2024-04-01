using StoreProject.BLL.Dtos.User;

namespace StoreProject.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsers();
        public Task<UserDto> GetUser(string id);
        public Task<UserDto> AddUser(UserRegisterDto newUserDto);
        public Task UpdateUser(UserDto userToUpdate);
        public Task<bool> DeleteUser(string id);
        public Task BuyProduct(string userId, string productId);
        //public Task<UserDto> loginUser(UserLoginDto userToLogin);
    }
}
