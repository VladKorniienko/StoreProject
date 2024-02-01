using AutoMapper;
using StoreProject.BLL.Dtos;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.AutoMapperProfiles
{
    public class ProductToProductDtoProfile : Profile
    {
        public ProductToProductDtoProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
