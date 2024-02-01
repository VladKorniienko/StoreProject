using AutoMapper;
using StoreProject.BLL.Dtos;
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

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _unitOfWork.Users.GetAll();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public UserDto GetUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public bool AddUser(UserLoginDto newUserDto, out UserDto createdUserDto, out string error)
        {
            //check wheter the user with the same email already exists in db
            if (_unitOfWork.Users.Find(u => u.Email == newUserDto.Email).Any())
            {
                error = "User with the same email already exists";
                createdUserDto = null;
                return false;
            }
            //if the user doesn't exist, create new user in db
            var newUser = _mapper.Map<User>(newUserDto);
            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();
            createdUserDto = _mapper.Map<UserDto>(_unitOfWork.Users.GetById(newUser.Id));
            error = "";
            return true;
        }

        public bool UpdateUser(UserDto userToUpdate, out string error)
        {

            if (!_unitOfWork.Users.Find(u => u.Id == userToUpdate.Id).Any())
            {
                error = "User not found";
                return false;
            }
            if (_unitOfWork.Users.Find(u => u.Email == userToUpdate.Email && u.Id != userToUpdate.Id).Any())
            {
                error = "User with the same email already exists";
                return false;
            }
            _unitOfWork.Users.Update(_mapper.Map<User>(userToUpdate));
            _unitOfWork.Save();
            error = "";
            return true;
        }

        public bool DeleteUser(int id, out string error)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                _unitOfWork.Save();
                error = "";
                return true;
            }
            error = "User not found";
            return false;
        }
    }
}
