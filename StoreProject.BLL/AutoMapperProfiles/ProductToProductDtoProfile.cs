using AutoMapper;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class ProductToProductDtoProfile : Profile
    {
        public ProductToProductDtoProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            CreateMap<UserPartialDto, User>().ReverseMap(); //necessary for nested mapping
        }
    }
}
