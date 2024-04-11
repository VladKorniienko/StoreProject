using AutoMapper;
using Microsoft.AspNetCore.Http;
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
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => Convert.ToBase64String(src.Icon)));
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<Product, ProductCreateOrUpdateDto>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ReverseMap();
            CreateMap<ProductCreateOrUpdateDto, Product>()
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.Icon)));

            CreateMap<UserPartialDto, User>().ReverseMap(); //necessary for nested mapping
            CreateMap<CategoryPartialDto, Category>().ReverseMap(); //necessary for nested mapping
            CreateMap<GenrePartialDto, User>().ReverseMap(); //necessary for nested mapping
        }
        private byte[] ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
 
    }
}
