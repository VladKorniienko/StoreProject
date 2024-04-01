using StoreProject.DAL.Models;
namespace StoreProject.DAL.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<IEnumerable<Product>> GetAllWithUsers();
    }
}
