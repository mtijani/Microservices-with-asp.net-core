using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.ShoppingCartAPI.Models.Dtos;

namespace TpAPP.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
      Task<CartDto>  GetCardByUserId(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
       Task <bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
