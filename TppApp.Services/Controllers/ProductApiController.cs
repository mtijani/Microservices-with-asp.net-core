using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TppApp.Services.ProductAPI.models.Dtos;
using TppApp.Services.ProductAPI.Repository;

namespace TppApp.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        protected ResponseDto _Response;
        private IProductRepository _productRepository;
        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._Response = new ResponseDto();
        }
     // [Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
                _Response.Result = productDtos;

            }
            catch (Exception e)
            {
                _Response.isSuccess = false;
                _Response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _Response;
        }

        [HttpGet]
     // [Authorize]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                _Response.Result = productDto;

            }
            catch (Exception e)
            {
                _Response.isSuccess = false;
                _Response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _Response;
        }
        [HttpPost]
   //   [Authorize]

        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _Response.Result = model;

            }
            catch (Exception e)
            {
                _Response.isSuccess = false;
                _Response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _Response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _Response.Result = model;

            }
            catch (Exception e)
            {
                _Response.isSuccess = false;
                _Response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _Response;
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _Response.Result = isSuccess;

            }
            catch (Exception e)
            {
                _Response.isSuccess = false;
                _Response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _Response;
        }


    }
}
