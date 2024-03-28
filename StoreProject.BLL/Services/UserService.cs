using AutoMapper;
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
            var users = _unitOfWork.Users.GetAllWithProducts();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public async Task<UserDto> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentNullException("User not found."); //change to custom NotFoundException
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
                throw new ArgumentException("User with the same email already exists.", nameof(newUserDto));
            }
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(newUserDto);
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<bool> UpdateUser(UserDto userToUpdate)
        {
            var existingUser = await _unitOfWork.Users.FindAsync(u => u.Id == userToUpdate.Id);
            if (!existingUser.Any())
            {
                throw new ArgumentNullException("User not found."); //change to custom NotFoundException
            }
            var userWithSameEmail = await _unitOfWork.Users.FindAsync(u => u.Email == userToUpdate.Email && u.Id != userToUpdate.Id);
            if (userWithSameEmail.Any())
            {
                throw new ArgumentException("User with the same email already exists.", nameof(userToUpdate));
            }
            await _unitOfWork.Users.UpdateAsync(_mapper.Map<User>(userToUpdate));
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentNullException("User not found."); //change to custom NotFoundException
            }
            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
