using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        public Task<Category> GetByIdWithProducts(string id);
    }
}
