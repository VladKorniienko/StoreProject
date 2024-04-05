using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.DAL.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext dbContext) : base(dbContext)
        {
        }
    }
}
