using Microsoft.Extensions.DependencyInjection;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using System.Reflection;
using FluentValidation;

namespace StoreProject.BLL
{
    public static class ServiceExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
