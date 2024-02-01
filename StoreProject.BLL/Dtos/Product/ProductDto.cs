using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? Genre { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public List<UserPartialDto>? Users { get; } = new();
    }
}
