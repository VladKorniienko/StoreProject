namespace StoreProject.DAL.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; } = 0.00M;
        public string GenreId { get; set; }
        public string CategoryId { get; set; }
        public string? Description { get; set; }
        public List<User>? Users { get; } = new();
        public Genre Genre { get; set; }
        public Category Category { get; set; }
    }
}
