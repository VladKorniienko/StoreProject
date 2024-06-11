using StoreProject.DAL.Models;
namespace StoreProject.DAL.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<IEnumerable<Product>> GetAllDetailsWithUsers(int pageNumber, int pageSize);
        public Task<Product> GetByIdWithAllDetails(string id);
        public Task<int> CountAsync();
    }
}
