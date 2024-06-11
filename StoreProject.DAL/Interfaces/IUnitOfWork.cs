namespace StoreProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IGenreRepository Genres { get; }
        ICategoryRepository Categories { get; }
        Task SaveAsync();
    }
}
