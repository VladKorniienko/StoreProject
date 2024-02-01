using StoreProject.DAL.Models;

namespace StoreProject.BLL.Dtos
{
    public class ProductDto
    { 
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? Genre { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public List<User>? Users { get; } = new();
    }
}
