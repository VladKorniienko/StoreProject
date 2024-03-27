using AutoMapper;
using StoreProject.BLL.Dtos.Product;
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

        public async Task <IEnumerable<ProductDto>> GetProducts()
        {
            var products = _unitOfWork.Products.GetAllWithUsers();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        // public bool AddProduct(ProductDto newProductDto, out ProductDto createdProductDto, out string error)
        public async Task<ProductDto> AddProduct(ProductDto newProductDto)
        {
            //check wheter the product with the same name already exists in db
            if (_unitOfWork.Products.Find(p => p.Name == newProductDto.Name).Any())
            {
                //error = "Product with the same name already exists";
                //createdProductDto = null;
                //return false;
                throw new ArgumentException("Product with the same name already exists.", nameof(newProductDto));
            }
            //if the product doesn't exist, create new product in db
            var newProduct = _mapper.Map<Product>(newProductDto);
            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.Save();
            //createdProductDto = _mapper.Map<ProductDto>(_unitOfWork.Products.GetById(newProduct.Id));
            //error = "";
            //return true;
            return _mapper.Map<ProductDto>(_unitOfWork.Products.GetById(newProduct.Id));
        }

        public async Task<bool> DeleteProduct(int id)
        {
            //check if the product exists in db
            if (!_unitOfWork.Products.Find(p => p.Id == id).Any())
            {
                throw new ArgumentNullException("Product not found. "); //change to custom NotFoundException
            }
            //if the product exists, delete it from db
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> UpdateProduct(ProductDto productToUpdate)
        {
            //check if the product with the same name already exists in db
            if (_unitOfWork.Products.Find(p => p.Name == productToUpdate.Name && p.Id != productToUpdate.Id).Any())
            {
                throw new ArgumentException("Product with the same name already exists. ", nameof(productToUpdate));
            }
            //if the product exists, update it in db
            _unitOfWork.Products.Update(_mapper.Map<Product>(productToUpdate));
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> AddUser(int productId, int userId)
        {
            var existingProduct = _unitOfWork.Products.GetById(productId);
            var existingUser = _unitOfWork.Users.GetById(userId);

            if (existingProduct == null || existingUser == null)
            {
                throw new ArgumentNullException("Product or User not found. "); //change to custom NotFoundException
            }
            else
            {
                existingProduct.Users.Add(existingUser);
                _unitOfWork.Products.Update(existingProduct);
                _unitOfWork.Save();
                return true;
            }
        }
    }
}
