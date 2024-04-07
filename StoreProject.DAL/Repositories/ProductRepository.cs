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
        public async Task<IEnumerable<Product>> GetAllDetailsWithUsers()
        {
            return await _dbContext.Products.Include(p => p.Users).Include(p => p.Genre).Include(p => p.Category).ToListAsync();
        }
        public async Task<Product> GetByIdWithAllDetails(string id)
        {
            return await _dbContext.Products.Where(p => p.Id == id).Include(p => p.Users).FirstOrDefaultAsync();
        }
    }
}
