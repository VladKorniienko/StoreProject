using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Data.Configurations;
using StoreProject.DAL.Models;


namespace StoreProject.DAL.Context
{
    public class StoreContext : IdentityDbContext<User>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Product config
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            //Genre config
            modelBuilder.ApplyConfiguration(new GenreConfiguration());

            //Category config
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            //User config
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
