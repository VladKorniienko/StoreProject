using Microsoft.EntityFrameworkCore;
using StoreProject.DAL.Context;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;

namespace StoreProject.DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(StoreContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllDetailsWithUsers()
        {
            return await _dbContext.Products.Include(p => p.Users).Include(p => p.Genre).Include(p => p.Category).ToListAsync();
        }
        public async Task<Product> GetByIdWithAllDetails(string id)
        {
            return await _dbContext.Products.Where(p => p.Id == id).Include(p => p.Users).FirstOrDefaultAsync();
        }

        public async Task AddProductWithGenreAndCategoryAsync(Product product, string genreName, string categoryName)
        {
            // Find or add genre
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                // Genre does not exist, create a new one
                genre = new Genre { Name = genreName };
                _dbContext.Genres.Add(genre);
            }

            // Find or add category
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
            if (category == null)
            {
                // Category does not exist, create a new one
                category = new Category { Name = categoryName };
                _dbContext.Categories.Add(category);
            }

            // Associate genre and category with the product
            product.Genre = genre;
            product.Category = category;

            // Add the product to the database context
            await AddAsync(product);
        }
    }
}
