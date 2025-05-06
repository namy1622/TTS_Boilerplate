using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TTS_boilerplate.Carts;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.Products;
using TTS_boilerplate.Web.Models.Carts;
using Abp.Runtime.Session;

namespace TTS_boilerplate.Web.Controllers
{
    public class CartsController : TTS_boilerplateControllerBase
    {
        private readonly ICartsAppService _cartService;
        private readonly IProductService _productService;
        public CartsController(ICartsAppService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> IndexCarts()
        {
            var userId = Convert.ToInt32(AbpSession.GetUserId()); // 23; // GetCurrentUserId();
            var allCartItem = (await _productService.Get_ListCartItem( userId)).Items;
            
            var model = new CartListViewModel
            {
                CartList = allCartItem
            };
            return View(model);
        }

    }
}
