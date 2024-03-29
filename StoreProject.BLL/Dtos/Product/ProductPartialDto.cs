namespace StoreProject.BLL.Dtos.Product
{
    public class ProductPartialDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceUSD { get; set; }
        public string? Genre { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
