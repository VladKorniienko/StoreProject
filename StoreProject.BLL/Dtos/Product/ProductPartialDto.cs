namespace StoreProject.BLL.Dtos.Product
{
    public class ProductPartialDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? GenreId { get; set; }
        public string? CategoryId { get; set; }
        public string? Description { get; set; }
    }
}
