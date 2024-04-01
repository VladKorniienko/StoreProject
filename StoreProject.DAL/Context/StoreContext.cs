using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Models;


namespace StoreProject.DAL.Context
{
    public class StoreContext : IdentityDbContext<User>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Product config
            modelBuilder.Entity<Product>()
                .Property(p => p.PriceUSD)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            //User config
            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            
        }
    }
}
