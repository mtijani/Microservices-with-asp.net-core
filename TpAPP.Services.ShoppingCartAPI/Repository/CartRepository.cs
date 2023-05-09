using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Services.ShoppingCartAPI.DbContexts;
using TpAPP.Services.ShoppingCartAPI.Models;
using TpAPP.Services.ShoppingCartAPI.Models.Dtos;

namespace TpAPP.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u =>
                u.UserId == userId);
            cartFromDb.CouponCode = couponCode;
            _db.CartHeader.Update(cartFromDb);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails
                    .RemoveRange(_db.CartDetails.Where(u=>u.CartHeaderId==cartHeaderFromDb.CartHeaderId));
                _db.CartHeader.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            // check if product exists in database, if not create it!
            var productInDb = await _db.Products
                .FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);
            if(productInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            //Check header is null
            var cartHeaderFromDb =  await _db.CartHeader.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeaderFromDb == null)
            {
                //create header and details 
                _db.CartHeader.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId  = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }


            
            else
            {
                //if header is not null 
                //check if details has some products
                var CartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId
                    &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId
                    );  //if it has then update the count 
            // else create details
                if (CartDetailsFromDb == null)
                {
                    //Create details 
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();

                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    //Update count /cart details
                    cart.CartDetails.FirstOrDefault().Count += CartDetailsFromDb.Count;
                    cart.CartDetails.FirstOrDefault().CartDetailsId += CartDetailsFromDb.CartDetailsId;
                    cart.CartDetails.FirstOrDefault().CartHeaderId += CartDetailsFromDb.CartHeaderId;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();   
                }
            }

            return _mapper.Map<CartDto>(cart);

        }

        public async Task<CartDto> GetCardByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
            };
           cart.CartDetails = _db.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u=>u.Product);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartFromDb = await _db.CartHeader.FirstOrDefaultAsync(u =>
                u.UserId == userId);
            cartFromDb.CouponCode = "";
            _db.CartHeader.Update(cartFromDb);
            await _db.SaveChangesAsync();
            return true;
        }

      

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails
                    .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);
                int totalCountOfCartItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeader
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeader.Remove(cartHeaderToRemove);
                 
                }
                await _db.SaveChangesAsync();
                return true;
            } catch(Exception e)
            {
                return false;
           
            }
        }
    }
}
