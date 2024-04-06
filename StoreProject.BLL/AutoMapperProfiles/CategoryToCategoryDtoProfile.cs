using AutoMapper;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class CategoryToCategoryDtoProfile : Profile
    {
        public CategoryToCategoryDtoProfile()
        {
            CreateMap<Category, CategoryPartialDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<ProductPartialDto, Product>().ReverseMap(); //necessary for nested mapping
        }
    }
}
