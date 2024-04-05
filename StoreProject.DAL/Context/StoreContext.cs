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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Product config
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                .Property(p => p.PriceUSD)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            //Genre config
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Genre)
                .WithMany(g => g.Products)
                .HasForeignKey(p => p.GenreId);
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<Genre>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Genre>()
                .HasIndex(p => p.Name)
                .IsUnique();

            //Category config
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
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
