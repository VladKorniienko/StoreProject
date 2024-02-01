using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(StoreContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<User> GetAllWithProducts()
        {
            return _dbContext.Users.Include(u => u.Products).ToList();
        }

        public IEnumerable<User> GetByIdWithProducts(int id)
        {
            return _dbContext.Users.Where(u => u.Id == id).Include(u => u.Products);
        }
    }
}
