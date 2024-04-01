﻿using StoreProject.BLL.Dtos.Product;

namespace StoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();
        public Task<ProductDto> GetProduct(string id);
        public Task<bool> AddUser(string productId, string userId);
        public Task<ProductDto> AddProduct(ProductDto newProductDto);
        public Task<bool> UpdateProduct(ProductDto productToUpdate);
        public Task<bool> DeleteProduct(string id);
    }
}
