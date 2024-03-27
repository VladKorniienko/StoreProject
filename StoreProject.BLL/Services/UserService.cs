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
            var user = _unitOfWork.Users.GetById(id);
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
            if (_unitOfWork.Users.Find(u => u.Email == newUserDto.Email).Any())
            {
                throw new ArgumentException("User with the same email already exists.", nameof(newUserDto));
            }
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(newUserDto);
            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();
            return _mapper.Map<UserDto>(_unitOfWork.Users.GetById(newUser.Id));
        }

        public async Task<bool> UpdateUser(UserDto userToUpdate)
        {
            if (!_unitOfWork.Users.Find(u => u.Id == userToUpdate.Id).Any())
            {
                throw new ArgumentNullException("User not found."); //change to custom NotFoundException
            }
            if (_unitOfWork.Users.Find(u => u.Email == userToUpdate.Email && u.Id != userToUpdate.Id).Any())
            {
                throw new ArgumentException("User with the same email already exists.", nameof(userToUpdate));
            }
            _unitOfWork.Users.Update(_mapper.Map<User>(userToUpdate));
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                throw new ArgumentNullException("User not found."); //change to custom NotFoundException
            }
            _unitOfWork.Users.Delete(user);
            _unitOfWork.Save();
            return true;
        }
    }
}
