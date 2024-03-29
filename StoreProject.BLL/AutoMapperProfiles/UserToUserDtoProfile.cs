﻿using AutoMapper;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class UserToUserDtoProfile : Profile
    {
        public UserToUserDtoProfile()
        {
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<ProductPartialDto, Product>().ReverseMap(); //necessary for nested mapping
        }        
    }
}
