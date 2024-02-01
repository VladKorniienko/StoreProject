using AutoMapper;
using StoreProject.BLL.Dtos;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class UserToUserDtoProfile : Profile
    {
        public UserToUserDtoProfile()
        {
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }        
    }
}
