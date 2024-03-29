using StoreProject.DAL.Models;

namespace StoreProject.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<IEnumerable<User>> GetAllWithProducts();
        public Task<User> GetByIdWithProducts(int id);
    }
}
