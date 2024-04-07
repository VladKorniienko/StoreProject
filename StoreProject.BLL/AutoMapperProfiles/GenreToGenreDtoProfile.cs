using AutoMapper;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Dtos.User;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class GenreToGenreDtoProfile : Profile
    {
        public GenreToGenreDtoProfile()
        {
            CreateMap<Genre, GenrePartialDto>().ReverseMap();
            CreateMap<Genre, GenreCreateOrUpdateDto>().ReverseMap();
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<GenreDto, Genre>()
                .ForMember(dest => dest.Products, opt => opt.Ignore());
            CreateMap<ProductPartialDto, Product>().ReverseMap(); //necessary for nested mapping
        }
    }
}
