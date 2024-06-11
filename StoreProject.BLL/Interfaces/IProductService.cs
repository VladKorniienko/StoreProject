using StoreProject.BLL.Dtos.Product;
using System.Drawing.Printing;

namespace StoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts(int pageNumber, int pageSize);
        public Task<ProductDto> GetProduct(string id);
        public Task<ProductDto> AddProduct(ProductCreateOrUpdateDto newProductDto);
        public Task UpdateProduct(ProductCreateOrUpdateDto productToUpdate, string id);
        public Task DeleteProduct(string id);
        public Task<int> GetTotalProducts();
    }
}
