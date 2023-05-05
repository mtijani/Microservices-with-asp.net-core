﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.MessageBus;
using TpAPP.Services.ShoppingCartAPI.Messages;
using TpAPP.Services.ShoppingCartAPI.Models.Dtos;
using TpAPP.Services.ShoppingCartAPI.Repository;

namespace TpAPP.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDto _response;
        private readonly ImessageBus _messageBus;
        public CartAPIController(ICartRepository cartRepository, ImessageBus messageBus)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDto();
            _messageBus = messageBus;

        }
        [HttpGet("GetCart/{userId}")]
        public async Task< object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCardByUserId(userId);
                _response.Result = cartDto;
            }
            catch(Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartdto)
        {
            try
            {
                CartDto cartDto_ = await _cartRepository.CreateUpdateCart(cartdto);
                _response.Result = cartDto_;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartdto)
        {
            try
            {
                CartDto cartDto_ = await _cartRepository.CreateUpdateCart(cartdto);
                _response.Result = cartDto_;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int CartId )
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(CartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeader)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCardByUserId(checkoutHeader.UserId);
                if(cartDto == null)
                {
                    return BadRequest();
                }
                // the message we want to send
                checkoutHeader.CartDetails = cartDto.CartDetails;
                // logic to add message to process order. 
                await _messageBus.PublishMessage(checkoutHeader, "checkoutmessagetopic");
                    //bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                    //_response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
