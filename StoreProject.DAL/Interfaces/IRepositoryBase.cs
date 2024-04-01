using System.Linq.Expressions;

namespace StoreProject.DAL.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<T> GetByIdAsync(string id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task DeleteAsync(string id);
    }
}
