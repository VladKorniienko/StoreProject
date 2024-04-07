using StoreProject.BLL.Dtos.Product;

namespace StoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
        public Task<ProductDto> GetProduct(string id);
        public Task<ProductDto> AddProduct(ProductCreateOrUpdateDto newProductDto);
        public Task UpdateProduct(ProductCreateOrUpdateDto productToUpdate, string id);
        public Task DeleteProduct(string id);
    }
}
