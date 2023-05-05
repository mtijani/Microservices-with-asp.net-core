using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TppApp.Services.ProductAPI.models;
using TppApp.Services.ProductAPI.models.Dtos;

namespace TppApp.Services.ProductAPI
{
    public class MappingConfig
    {   
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
