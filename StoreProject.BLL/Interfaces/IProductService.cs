using StoreProject.BLL.Dtos.Product;

namespace StoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<ProductDto> GetProducts();
        public ProductDto GetProduct(int id);
        public bool AddUser(int productId, int userId, out string error);
        public bool AddProduct(ProductDto newProductDto, out ProductDto createdProductDto, out string error);
        public bool UpdateProduct(ProductDto productToUpdate, out string error);
        public bool DeleteProduct(int id, out string error);
    }
}
