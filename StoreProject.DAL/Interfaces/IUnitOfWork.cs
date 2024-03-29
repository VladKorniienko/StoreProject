namespace StoreProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}
