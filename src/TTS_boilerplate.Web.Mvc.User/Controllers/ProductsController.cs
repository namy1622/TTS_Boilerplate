using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Models;
using TTS_boilerplate.Orders;
using TTS_boilerplate.Orders.Dto;
using TTS_boilerplate.Products;
using TTS_boilerplate.Products.Dto;
using TTS_boilerplate.Web.Models.Products;

namespace TTS_boilerplate.Web.Controllers
{
    //[AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductsController : TTS_boilerplateControllerBase
    {
        private readonly IProductService _productService;

        private readonly ILookupAppService _lookupAppService;

        private readonly IOrdersAppService _orderAppService;

        public CartsController _cartController;

        public ProductsController(
            IProductService productService,
            ILookupAppService lookupAppService,
            IOrdersAppService orderAppService,
            CartsController cartController
            )
        {
            _productService = productService;
            _lookupAppService = lookupAppService;
            _orderAppService = orderAppService;

            _cartController = cartController;
        }

        public async Task<ActionResult> Index()
        {
            var currentUserId = 23; // _cartController.GetCurrentUserId(); 
            var allCategories = await _productService.GetCategory(); // Lấy danh sách categories
            await _productService.InitCart(currentUserId);
            var categorySelectListItems = (await _lookupAppService.GetCategoryComboboxItem()).Items
               .Select(c => c.ToSelectListItem())
               .ToList();
            //var allCategory = await _productService.Get();
            //var model = new IndexViewModel(allProduct.Items, categorySelectListItems);
            var model = new IndexViewModel(categorySelectListItems);
            ViewBag.Categories = allCategories.Items;
            return View(model);
            //return Ok();

        }

        //[HttpPost]
        public async Task<ActionResult> InitOrder(int productId)
        {
            await InitO();

            var currentUserId = Convert.ToInt32(AbpSession.GetUserId());// 23; // _cartController.GetCurrentUserId();
            await AddProductToCart(productId, currentUserId);
            //return PartialView("Index_Order");
            return RedirectToAction("Index_Order", "Orders", new { ProductId  = productId}); 
        }

        public async System.Threading.Tasks.Task InitO()
        {
            //var initOrderInput = new OrderInput()
            //{
            //    UserId = Convert.ToInt32(AbpSession.GetUserId()),
            //};
            //await _orderAppService.InitOrder(initOrderInput);

            int UserId = Convert.ToInt32(AbpSession.GetUserId());
           
            await _productService.InitCart(UserId);

        }
        public async System.Threading.Tasks.Task AddProductToCart(int ProductId, int currentUserId)
        {
            var addProductToOrderInput = new CartInput()
            {
                idUser = currentUserId,
                idProduct = ProductId,
                Status = "Pending",
            };
            await _productService.AddProductToCart(addProductToOrderInput);
        }
    } 
}