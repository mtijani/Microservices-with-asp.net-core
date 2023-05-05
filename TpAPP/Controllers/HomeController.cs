using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TpAPP.Models;
using TpAPP.Services.IServices;

//using TppApp.Models;

namespace TpAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _poriductService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService poriductService, ICartService cartService )
        {
            _logger = logger;
            _poriductService = poriductService;
            _cartService = cartService;

        }

        public async Task<IActionResult> Index()
        {
            //ProductDto pd = new ProductDto();
            List<ProductDto> list = new ();
            var response = await _poriductService.GetAllProductsAsync<ResponseDto>("");
            if(response!=null && response.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize]
        public async Task<IActionResult> Details(int ProductId)
        {
            //ProductDto pd = new ProductDto();
            ProductDto model = new();
            var response = await _poriductService.GetProductByIdAsync<ResponseDto>(ProductId, "");
            if (response != null && response.isSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
         

            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault().Value
                }
            };
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };
            var res = await _poriductService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
            if(res!=null && res.isSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(res.Result));
            }
            List<CartDetailsDto> cartDetailsDto = new();
            cartDetailsDto.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDto;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await _cartService.AddToCartAsync<ResponseDto>(cartDto, accessToken);
            if (addToCartResp != null && addToCartResp.isSuccess)
            {
                // cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(addToCartResp.Result));
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task<IActionResult> Login()
        {
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies","oidc");
        }
    }
}
