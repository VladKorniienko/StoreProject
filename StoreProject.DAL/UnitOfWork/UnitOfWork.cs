using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Repositories;

namespace StoreProject.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreContext _dbContext;
        private bool _disposedValue = false;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            Products = new ProductRepository(_dbContext);
            Users = new UserRepository(_dbContext);
            Genres = new GenreRepository(_dbContext);
            Categories = new CategoryRepository(_dbContext);
        }

        public IProductRepository Products { get; private set; }
        public IUserRepository Users { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public ICategoryRepository Categories { get; private set; }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
