using Microsoft.Extensions.DependencyInjection;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StoreProject.DAL.Models;
using StoreProject.DAL.Context;

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
            services.AddScoped<UserManager<User>>();
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<StoreContext>();


        }
    }
}
