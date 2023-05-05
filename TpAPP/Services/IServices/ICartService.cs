using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Models;

namespace TpAPP.Services.IServices
{
   public  interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token=null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCart<T>( int cartId, string token = null);
        Task<T> RemoveCoupon<T>(string userId, string token = null);

        Task<T> Checkout<T>(CartHeaderDto cartHeader, string token = null);




    }
}
