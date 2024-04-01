using AutoMapper;
using StoreProject.Common.Exceptions;
using StoreProject.BLL.Dtos.User;
using StoreProject.BLL.Interfaces;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StoreProject.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userManager.Users.Include(u => u.Products).ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public async Task<UserDto> GetUser(string id)
        {
            //var user = await _unitOfWork.Users.GetByIdWithProducts(id);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> AddUser(UserRegisterDto newUserDto)
        {
            //check wheter the user with the same email already exists in db
            //var existingUser = await _unitOfWork.Users.FindAsync(u => u.Email == newUserDto.Email);
            var existingUser = await _userManager.FindByEmailAsync(newUserDto.Email!);
            if (existingUser != null)
            {
                throw new ArgumentException($"User with the same email ({newUserDto.Email}) already exists.", nameof(newUserDto));
            }
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(newUserDto);
            var result = await _userManager.CreateAsync(newUser, newUserDto.Password!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task UpdateUser(UserUpdateDto userToUpdate, string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(userToUpdate.Email!);
            if (userWithSameEmail != null && userWithSameEmail.Id != id)
            {
                throw new ArgumentException($"User with the same email ({userToUpdate.Email}) already exists.");
            }
            _mapper.Map(userToUpdate, existingUser);

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.");
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
            return true;
        }

        public async Task BuyProduct(string userId, string productId)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(productId);
            var existingUser = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Products).FirstOrDefaultAsync();

            if (existingProduct == null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }
            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            if (existingUser.Products.Any(p => p.Id == existingProduct.Id))
            {
                throw new InvalidOperationException($"User with ID {userId} already has a product with ID {productId}");
            }
            if (existingUser.Balance < existingProduct.PriceUSD)
            {
                throw new InvalidOperationException($"User with ID {userId} has insufficient balance {existingUser.Balance}");
            }
            else
            {
                existingUser.Balance -= existingProduct.PriceUSD;
                existingUser.Products.Add(existingProduct);

                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new ArgumentException(string.Join(" ", errors));
                }
            }
        }
        /*
        public async Task<UserDto> LoginUser(UserLoginDto userLoginDto)
        {
            if (userLoginDto.Email == null && userLoginDto.Password == null)
            {
                throw new ArgumentException($"No login details specified.");
            }
            var user = await _unitOfWork.Users.FindAsync(u => u.Email == userLoginDto.Email);
            if (user.FirstOrDefault() == null)
            {
                throw new NotFoundException($"User with email {userLoginDto.Email} not found.");
            }





        }*/
    }
}
