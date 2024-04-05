namespace StoreProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        IGenreRepository Genres { get; }
        ICategoryRepository Categories { get; }
        Task SaveAsync();
    }
}
