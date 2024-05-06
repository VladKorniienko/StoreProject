using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;

namespace StoreProject.BLL.Dtos.Product
{
    public class ProductPartialDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public GenrePartialDto Genre { get; set; }
        public CategoryPartialDto Category { get; set; }
        public string? Description { get; set; }
        public List<string>? Screenshots { get; set; }
        public string? Icon { get; set; }
    }
}
