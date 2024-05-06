using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;

namespace StoreProject.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(StoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllWithProducts()
        {
            return await _dbContext.Users.Include(u => u.Products).ToListAsync();
        }

        public async Task<User> GetByIdWithProducts(string id)
        {
            return await _dbContext.Users
                .Include(u => u.Products)
                .ThenInclude(p => p.Genre)
                .Include(u => u.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
