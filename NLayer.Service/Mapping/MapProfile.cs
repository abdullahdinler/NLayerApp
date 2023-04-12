using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    // MapProfile içerisindeki CreateMap ile dönüşüm işlemleri yapıldı.
    // 
    // CreateMap<Source, Destination>()
    // 
    // Source: Dönüşüm yapılacak nesne türü
    // Destination: Dönüşüm sonrası nesne türü
    // 
    // ReverseMap() ile iki yönlü dönüşüm sağlandı.
    // 
    // Örnek: Product nesnesini ProductDto'ya dönüştürmek için CreateMap<Product, ProductDto>() kullanıldı.
    // 
    // Örnek: ProductDto nesnesini Product'a dönüştürmek için CreateMap<ProductDto, Product>() kullanıldı.
    // 
    // Örnek: Product nesnesini ProductUpdateDto'ya dönüştürmek için CreateMap<Product, ProductUpdateDto>() kullanıldı.
    // 
    // Örnek: ProductUpdateDto nesnesini Product'a dönüştürmek için CreateMap<ProductUpdateDto, Product>() kullanıldı.
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
        }
    } 
}
