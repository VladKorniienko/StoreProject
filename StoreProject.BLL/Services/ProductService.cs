using AutoMapper;
using StoreProject.Common.Exceptions;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Interfaces;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace StoreProject.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCreateOrUpdateDto> _productValidator;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductCreateOrUpdateDto> productValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.GetAllDetailsWithUsers(pageNumber, pageSize);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var product = await _unitOfWork.Products.GetByIdWithAllDetails(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> AddProduct(ProductCreateOrUpdateDto newProductDto)
        {
            await CheckValidation(newProductDto);
            //check whether the product with the same name already exists in db
            await CheckIfDuplicateNameExists(newProductDto.Name);
            // find genre
            var genre = await CheckIfGenreExists(newProductDto.GenreId);
            // find category
            var category = await CheckIfCategoryExists(newProductDto.CategoryId);

            var newProduct = _mapper.Map<Product>(newProductDto);
            
            // associate genre and category with the product
            newProduct.Genre = genre;
            newProduct.Category = category;

            await _unitOfWork.Products.AddAsync(newProduct);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductDto>(newProduct);
        }

        public async Task DeleteProduct(string id)
        {
            //check if the product exists in db
            var product = await CheckIfProductExists(id);
            //if the product exists, delete it from db
            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProduct(ProductCreateOrUpdateDto productToUpdate, string id)
        {
            await CheckValidation(productToUpdate);

            var existingProduct = await CheckIfProductExists(id);
            //check if the product with the same name already exists in db
            await CheckIfDuplicateNameExists(productToUpdate.Name, id);
            // Find genre
            var genre = await CheckIfGenreExists(productToUpdate.GenreId);
            // Find category
            var category = await CheckIfCategoryExists(productToUpdate.CategoryId);
            _mapper.Map(productToUpdate, existingProduct);
            // Associate genre and category with the product
            existingProduct.Genre = genre;
            existingProduct.Category = category;
            //if the product exists, update it in db
            await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveAsync();
        }
        private async Task CheckIfDuplicateNameExists(string name, string id = null)
        {
            var productsWithSameName = await _unitOfWork.Products.FindAsync(p => p.Name == name && (id == null || p.Id != id));
            if (productsWithSameName.Any())
            {
                throw new ArgumentException($"Product with the same name ({name}) already exists.");
            }
        }
        private async Task<Genre> CheckIfGenreExists(string id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                // Genre does not exist
                throw new ArgumentException($"Genre with the ID ({id}) doesn't exist.");
            }
            return genre;
        }
        private async Task<Category> CheckIfCategoryExists(string id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                // Category does not exist
                throw new ArgumentException($"Category with the ID ({id}) doesn't exist.");
            }
            return category;
        }
        private async Task CheckValidation(ProductCreateOrUpdateDto productToValidate)
        {
            var validationResultForProduct = await _productValidator.ValidateAsync(productToValidate);
            if (!validationResultForProduct.IsValid)
            {
                var errorMessage = string.Join(" ", validationResultForProduct.Errors);
                throw new ArgumentException(errorMessage);
            }
        }
        private async Task<Product> CheckIfProductExists(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with the ID {id} doesn't exist.");
            }
            return product;
        }
        public async Task<int> GetTotalProducts()
        {
            return await _unitOfWork.Products.CountAsync();
        }
    }
}
