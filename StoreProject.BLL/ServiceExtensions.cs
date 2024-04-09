using Microsoft.Extensions.DependencyInjection;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StoreProject.DAL.Models;
using StoreProject.DAL.Context;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StoreProject.BLL
{
    public static class ServiceExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<UserManager<User>>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddRoles<IdentityRole>()
              .AddRoleManager<RoleManager<IdentityRole>>()
              .AddRoleStore<RoleStore<IdentityRole, StoreContext>>()
              .AddTokenProvider<DataProtectorTokenProvider<User>>("REFRESHTOKENPROVIDER")
              .AddEntityFrameworkStores<StoreContext>();
            //all three AddRoles,AddRoleManager,AddRoleStore are required for the app to function if
            //AddIdentityCore is used instead of AddIdentity

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromSeconds(Convert.ToDouble(configuration["JwtSettings:RefreshTokenExpireTimeSeconds"]));
            });

        }
    }
}
