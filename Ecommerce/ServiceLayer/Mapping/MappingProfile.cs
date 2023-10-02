using AutoMapper;
using DomainLayer.Entities;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ForMember(x => x.ProductCount, y => y.MapFrom(z => z.Products.Count()));
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.Name))
                .ForMember(x => x.Color, y => y.MapFrom(z => z.ProductDetail.Color))
                .ForMember(x => x.Design, y => y.MapFrom(z => z.ProductDetail.Design))
                .ForMember(x => x.Material, y => y.MapFrom(z => z.ProductDetail.Material))
                .ForMember(x => x.About, y => y.MapFrom(z => z.ProductDetail.About));
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactCreateDto>().ReverseMap();

            CreateMap<ProductDetail, ProductDetailDto>();
            CreateMap<ProductDetailDto, ProductDetail>();
            CreateMap<ProductDetail, ProductDetailCreateDto>().ReverseMap();
            CreateMap<ProductDetail, ProductDetailUpdateDto>().ReverseMap();

            CreateMap<ProductComment, ProductCommentDto>().ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.Name));
            CreateMap<ProductCommentDto, ProductComment>();
            CreateMap<ProductComment, ProductCommentCreateDto>().ReverseMap();
        }
    }
}
