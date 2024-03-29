using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Repositories;
namespace StoreProject.DAL
{
    public static class ServiceExtensions
    {
        public static void ConfigureDAL(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, StoreProject.DAL.UnitOfWork.UnitOfWork>();

            services.AddDbContext<StoreContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("UserContext")));
        }
    }
}
