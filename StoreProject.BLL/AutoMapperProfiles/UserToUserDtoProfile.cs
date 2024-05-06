using AutoMapper;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class UserToUserDtoProfile : Profile
    {
        public UserToUserDtoProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap <User,UserUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<User, UserInfoWithRoleDto>().
                ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<UserInfoWithRoleDto, User>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());

            //necessary for nested mapping
            CreateMap<Product, ProductPartialDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => Convert.ToBase64String(src.Icon)))
                .ForMember(dest => dest.Screenshots, opt => opt
                .MapFrom(src => src.Screenshots.Select(screenshot => Convert.ToBase64String(screenshot)).ToList()));
            CreateMap<ProductPartialDto, Product>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
        }
    }
}
