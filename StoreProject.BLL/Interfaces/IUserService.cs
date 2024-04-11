using StoreProject.BLL.Dtos.User;

namespace StoreProject.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInfoWithRoleDto>> GetUsers();
        //public IAsyncEnumerable<UserInfoWithRoleDto> GetUsers();
        public Task<UserInfoWithRoleDto> GetUser(string id);
        public Task UpdateUser(UserUpdateDto userToUpdate, string id);
        public Task DeleteUser(string id);
        public Task BuyProduct(string userId, string productId);
    }
}
