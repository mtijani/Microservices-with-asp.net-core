using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TpAPP.Services.ShoppingCartAPI.Models.Dtos;

namespace TpAPP.Services.ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient client;
        public CouponRepository(HttpClient client)
        {
            this.client = client;
        }
        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var respone = await client.GetAsync($"/api/coupon/{couponName}");
            var apiContent = await respone.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.isSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
