using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;

namespace StoreProject.DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(StoreContext dbContext) : base(dbContext)
        {
        }
        public IEnumerable<Product> GetAllWithUsers()
        {
            return _dbContext.Products.Include(p => p.Users).ToList();
        }
    }
}
