using StoreProject.BLL.Dtos.Product;

namespace StoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
        public Task<ProductDto> GetProduct(int id);
        public Task<bool> AddUser(int productId, int userId);
        public Task<ProductDto> AddProduct(ProductDto newProductDto);
        public Task<bool> UpdateProduct(ProductDto productToUpdate);
        public Task<bool> DeleteProduct(int id);
    }
}
