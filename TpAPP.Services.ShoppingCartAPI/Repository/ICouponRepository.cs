using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.ShoppingCartAPI.Models.Dtos;

namespace TpAPP.Services.ShoppingCartAPI.Repository
{
   public   interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);

    }
}
