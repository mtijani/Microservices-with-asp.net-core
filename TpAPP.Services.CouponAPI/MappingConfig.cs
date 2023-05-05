using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.CouponAPI.Models;
using TpAPP.Services.CouponAPI.Models.Dto;

namespace TpAPP.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
                //config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                //config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                //config.CreateMap<Cart, CartDto>().ReverseMap();
                //config.CreateMap<Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
