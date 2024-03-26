using BusinessLogic.DTOs;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            //Map Product => ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(productdto => productdto.CategoryName, opt => opt.MapFrom(product => product.Category.Name));
            //Map ProductDto => Product
            CreateMap<ProductDto, Product>();

            CreateMap<Product, EditProductDto>()
          .ForMember(productDto => productDto.CategoryName, opt => opt.MapFrom(product => product.Category.Name));

            CreateMap<Product, EditProductDto>()
          .ForMember(productDto => productDto.Image, opt => opt.Ignore());
            //map ProductDto=>Product
            CreateMap<EditProductDto, Product>();
        }
    }
}
