using AutoMapper;
using StoreProject.BLL.Dtos;
using StoreProject.BLL.Interfaces;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _unitOfWork.Products.GetAllWithUsers();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public ProductDto GetProduct(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public bool AddProduct(ProductDto newProductDto, out ProductDto createdProductDto, out string error)
        {
            //check wheter the product with the same name already exists in db
            if (_unitOfWork.Products.Find(p => p.Name == newProductDto.Name).Any())
            {
                error = "Product with the same name already exists";
                createdProductDto = null;
                return false;
            }
            //if the product doesn't exist, create new product in db
            var newProduct = _mapper.Map<Product>(newProductDto);
            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.Save();
            createdProductDto = _mapper.Map<ProductDto>(_unitOfWork.Products.GetById(newProduct.Id));
            error = "";
            return true;
        }

        public bool DeleteProduct(int id, out string error)
        {
            //check if the product exists in db
            if (!_unitOfWork.Products.Find(p => p.Id == id).Any())
            {
                error = "Product not found";
                return false;
            }
            //if the product exists, delete it from db
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
            error = "";
            return true;
        }

        public bool UpdateProduct(ProductDto productToUpdate, out string error)
        {
            //check if the product exists in db
            if (!_unitOfWork.Products.Find(p => p.Id == productToUpdate.Id).Any())
            {
                error = "Product not found";
                return false;
            }
            //check if the product with the same name already exists in db
            if (_unitOfWork.Products.Find(p => p.Name == productToUpdate.Name && p.Id != productToUpdate.Id).Any())
            {
                error = "Product with the same name already exists";
                return false;
            }
            //if the product exists, update it in db
            _unitOfWork.Products.Update(_mapper.Map<Product>(productToUpdate));
            _unitOfWork.Save();
            error = "";
            return true;
        }

        public bool AddUser(int productId, int userId, out string error)
        {
            var existingProduct = _unitOfWork.Products.GetById(productId);
            var existingUser = _unitOfWork.Users.GetById(userId);

            if (existingProduct == null || existingUser == null)
            {
                error = "Product or user not found";
                return false;
            }
            else
            {
                existingProduct.Users.Add(existingUser);
                _unitOfWork.Products.Update(existingProduct);
                _unitOfWork.Save();
                var a = _unitOfWork.Products.GetById(productId);
                error = "";
                return true;
            }
        }
    }
}
