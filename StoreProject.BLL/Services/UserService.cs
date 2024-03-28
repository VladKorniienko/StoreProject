using AutoMapper;
using StoreProject.Common.Exceptions;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAllWithProducts();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public async Task<UserDto> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdWithProducts(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> AddUser(UserLoginDto newUserDto)
        {
            //check wheter the user with the same email already exists in db
            var existingUser = await _unitOfWork.Users.FindAsync(u => u.Email == newUserDto.Email);
            if (existingUser.Any())
            {
                throw new ArgumentException($"User with the same email ({newUserDto.Email}) already exists.", nameof(newUserDto));
            }
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(newUserDto);
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task UpdateUser(UserDto userToUpdate)
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(userToUpdate.Id);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userToUpdate.Id} not found.");
            }

            var userWithSameEmail = await _unitOfWork.Users.FindAsync(u => u.Email == userToUpdate.Email && u.Id != userToUpdate.Id);
            if (userWithSameEmail.Any())
            {
                throw new ArgumentException($"User with the same email ({userToUpdate.Email}) already exists.", nameof(userToUpdate));
            }
            _mapper.Map(userToUpdate, existingUser);
            await _unitOfWork.Users.UpdateAsync(existingUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }
            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task BuyProduct(int userId, int productId)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productId);
            var existingUser = await _unitOfWork.Users.GetByIdWithProducts(userId);

            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            else if (existingUser.Products.Any(p => p.Id == existingProduct.Id))
            {
                throw new InvalidOperationException($"User with ID {userId} already has a product with ID {productId}");
            }
            else if (existingUser.Balance < existingProduct.PriceUSD)
            {
                throw new InvalidOperationException($"User with ID {userId} has insufficient balance {existingUser.Balance}");
            }
            else
            {
                existingUser.Balance -= existingProduct.PriceUSD;
                existingUser.Products.Add(existingProduct);

                await _unitOfWork.Users.UpdateAsync(existingUser);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
