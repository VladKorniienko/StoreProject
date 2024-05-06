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

        //public async IAsyncEnumerable<UserInfoWithRoleDto> GetUsers()
        //{
        //    var users = _userManager.Users.Include(u => u.Products).AsAsyncEnumerable();
      
        //    await foreach(var user in users) 
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var userDto = _mapper.Map<UserInfoWithRoleDto>(user);
        //        userDto.Role = userRoles.Contains("Admin") ? "Admin" : "User";
        //        yield return userDto;
        //    }
        //}
        public async Task<IEnumerable<UserInfoWithRoleDto>> GetUsers()
        {
            var users = _userManager.Users.Include(u => u.Products);
            var usersDto = new List<UserInfoWithRoleDto>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserInfoWithRoleDto>(user);
                userDto.Role = userRoles.Contains("Admin") ? "Admin" : "User";
                usersDto.Add(userDto);
            }
            return usersDto;
        }
        public async Task<UserInfoWithRoleDto> GetUser(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Products)
                .ThenInclude(p => p.Genre)
                .Include(u => u.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.Id == id);
            var userDto = _mapper.Map<UserInfoWithRoleDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Role = roles.Contains("Admin") ? "Admin" : "User";
            return userDto;

        }
        public async Task UpdateUser(UserUpdateDto userToUpdate, string id)
        {
            var existingUser = await CheckIfUserExists(id);
            await CheckIfDuplicateEmailExists(userToUpdate.Email, id);
            _mapper.Map(userToUpdate, existingUser);

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
        }

        public async Task DeleteUser(string id)
        {
            var user = await CheckIfUserExists(id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ArgumentException(string.Join(" ", errors));
            }
        }

        public async Task BuyProduct(string userId, string productId)
        {
            var existingProduct = await CheckIfProductExists(productId);
            var existingUser = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Products).FirstOrDefaultAsync();

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

        private async Task<Product> CheckIfProductExists(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with the ID {id} doesn't exist.");
            }
            return product;
        }
        private async Task<User> CheckIfUserExists(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with the ID {id} doesn't exist.");
            }
            return user;
        }
        private async Task CheckIfDuplicateNameExists(string name, string id = null)
        {
            var productsWithSameName = await _unitOfWork.Products.FindAsync(p => p.Name == name && (id == null || p.Id != id));
            if (productsWithSameName.Any())
            {
                throw new ArgumentException($"Product with the same name ({name}) already exists.");
            }
        }
        private async Task CheckIfDuplicateEmailExists(string email, string id = null)
        {
            var existingUser = await _userManager.Users.Where(u => u.Email == email && (id == null || u.Id != id)).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new ArgumentException($"User with the same email ({email}) already exists.");
            }
        }
    }
}
