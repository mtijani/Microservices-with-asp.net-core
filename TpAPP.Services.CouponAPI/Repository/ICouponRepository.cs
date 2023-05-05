using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.CouponAPI.Models.Dto;

namespace TpAPP.Services.CouponAPI.Repository
{
   public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
