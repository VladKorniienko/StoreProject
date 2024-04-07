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
        private readonly IValidator<Product> _productValidator;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Product> productValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task <IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAllDetailsWithUsers();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> AddProduct(ProductCreateDto newProductDto)
        {
            //check wheter the product with the same name already exists in db
            var product = await _unitOfWork.Products.FindAsync(p => p.Name == newProductDto.Name);
            if (product.Any())
            {
                throw new ArgumentException($"Product with the same name ({newProductDto.Name}) already exists.");
            }
            // Find genre
            var genre = await _unitOfWork.Genres.GetByIdAsync(newProductDto.GenreId);
            if (genre == null)
            {
                // Genre does not exist
                throw new ArgumentException($"Genre with the ID ({newProductDto.GenreId}) doesn't exist.");
            }
            // Find category
            var category = await _unitOfWork.Categories.GetByIdAsync(newProductDto.CategoryId);
            if (category == null)
            {
                // Category does not exist
                throw new ArgumentException($"Category with the ID ({newProductDto.CategoryId}) doesn't exist.");
            }

            var newProduct = _mapper.Map<Product>(newProductDto);

            // Associate genre and category with the product
            newProduct.Genre = genre;
            newProduct.Category = category;

            //if the product doesn't exist, create new product in db
            var validationResultForProduct = await _productValidator.ValidateAsync(newProduct);
            if (!validationResultForProduct.IsValid)
            {
                var errorMessage = string.Join(" ", validationResultForProduct.Errors);
                throw new ArgumentException(errorMessage);
            }
            await _unitOfWork.Products.AddAsync(newProduct);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductDto>(newProduct);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            //check if the product exists in db
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product==null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            //if the product exists, delete it from db
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateProduct(ProductCreateDto productToUpdate, string id)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }
            //check if the product with the same name already exists in db
            var productWithNameDuplicate = await _unitOfWork.Products.FindAsync(p => p.Name == productToUpdate.Name && p.Id != id );
            if (productWithNameDuplicate.Any())
            {
                throw new ArgumentException($"Product with the same name ({productToUpdate.Name}) already exists.");
            }
            // Find genre
            var genre = await _unitOfWork.Genres.GetByIdAsync(productToUpdate.GenreId);
            if (genre == null)
            {
                // Genre does not exist
                throw new ArgumentException($"Genre with the ID ({productToUpdate.GenreId}) doesn't exist.");
            }
            // Find category
            var category = await _unitOfWork.Categories.GetByIdAsync(productToUpdate.CategoryId);
            if (category == null)
            {
                // Category does not exist
                throw new ArgumentException($"Category with the ID ({productToUpdate.CategoryId}) doesn't exist.");
            }

            _mapper.Map(productToUpdate, existingProduct);
            // Associate genre and category with the product
            existingProduct.Genre = genre;
            existingProduct.Category = category;
            //if the product exists, update it in db
            
            var validationResultForProduct = await _productValidator.ValidateAsync(existingProduct);
            if (!validationResultForProduct.IsValid)
            {
                var errorMessage = string.Join(Environment.NewLine, validationResultForProduct.Errors);
                throw new ArgumentException(errorMessage);
            }
            await _unitOfWork.Products.UpdateAsync(existingProduct);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> AddUser(string productId, string userId)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productId);
            var existingUser = await _unitOfWork.Users.GetByIdAsync(userId);

            if (existingProduct == null) 
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            } 
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userId} not found."); 
            }
            else
            {
                existingProduct.Users.Add(existingUser);
                await _unitOfWork.Products.UpdateAsync(existingProduct);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
    }
}
