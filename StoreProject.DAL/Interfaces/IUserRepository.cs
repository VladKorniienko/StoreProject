using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public Task<IEnumerable<User>> GetAllWithProducts();
        public Task<User> GetByIdWithProducts(int id);
    }
}
