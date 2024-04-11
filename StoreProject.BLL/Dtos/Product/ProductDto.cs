using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Dtos.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public GenrePartialDto Genre { get; set; }
        public CategoryPartialDto Category { get; set; }
        public List<UserPartialDto> Users { get; } = new();
    }
}
