using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TTS_boilerplate.Carts;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.Orders;
using TTS_boilerplate.Orders.Dto;
using TTS_boilerplate.Products;
using TTS_boilerplate.Web.Models.Orders;

namespace TTS_boilerplate.Web.Controllers
{
    public class OrdersController : TTS_boilerplateControllerBase
    {
        private readonly IOrdersAppService _orderAppService;
        private readonly IProductService _productService;
        public readonly ICartsAppService _cartService;

        public OrdersController(IOrdersAppService ordersAppService, IProductService productService, ICartsAppService cartService)
        {
          _orderAppService = ordersAppService;
            _productService = productService;
            _cartService = cartService;
        }
        public async System.Threading.Tasks.Task<IActionResult> Index_Order(int ProductId)
        {
            var product = await _productService.GetProduct(ProductId);
            //var cart = await _cartService.get;
            var quantity = await _orderAppService.GetItemOrder(ProductId);
            var model = new OrderItemViewModel
            {
                Product = product,
                Quantity = quantity,
            };
            return View(model);
        }

        
        public async Task GetViewOrder()
        {

        }


    }
}
