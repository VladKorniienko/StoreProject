using AutoMapper;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class ProductToProductDtoProfile : Profile
    {
        public ProductToProductDtoProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<Product, ProductCreateOrUpdateDto>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ReverseMap();

            CreateMap<UserPartialDto, User>().ReverseMap(); //necessary for nested mapping
            CreateMap<CategoryPartialDto, Category>().ReverseMap(); //necessary for nested mapping
            CreateMap<GenrePartialDto, User>().ReverseMap(); //necessary for nested mapping
        }
    }
}
