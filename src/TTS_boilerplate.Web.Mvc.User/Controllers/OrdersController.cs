using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public CartsController _cartController;

        public OrdersController(IOrdersAppService ordersAppService, IProductService productService, CartsController cartController)
        {
          _orderAppService = ordersAppService;
            _productService = productService;
            _cartController = cartController;
        }
        public async System.Threading.Tasks.Task<ActionResult> Index_Order(int ProductId)
        {
            //var currentUserId = _cartController.GetCurrentUserId(); 

            //await InitOrder();

            //await AddProductToCart(ProductId);

            ////GetViewOrder();

            return View();
        }

        //public async Task InitOrder()
        //{
        //    var initOrderInput = new OrderInput()
        //    {
        //        UserId = 1,
        //    };
        //    await _orderAppService.InitOrder(initOrderInput);
        //}

        //public async Task AddProductToCart(int ProductId)
        //{
        //    var addProductToOrderInput = new CartInput()
        //    {
        //        idUser = 1,
        //        idProduct = ProductId,
        //        Status = "Pending",
        //    };
        //    await _productService.AddProductToCart(addProductToOrderInput);
        //}

        public async Task GetViewOrder()
        {

        }





    }
}
