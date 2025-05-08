using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTS_boilerplate.Carts;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.Models;
using TTS_boilerplate.Orders;
using TTS_boilerplate.Orders.Dto;
using TTS_boilerplate.Products;
using TTS_boilerplate.Products.Dto;
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
        //=================================================
        //[HttpPost]
        [Route("Orders")]
        public async System.Threading.Tasks.Task<IActionResult> Index_Order(int? ProductId,  List<OrderTrans>? selectedItems)
        {

            var orderItemViewModel = new OrderItemViewModel
            {
                ProductList = new List<CartItemDto>(),
                CartList = new List<OrderItemDto>()
            };

            if (ProductId.HasValue)
            {
                var product = await _productService.Get_CartItem(ProductId);
                if(product == null)
                {
                    return BadRequest("San pham khong ton tai!!!");
                }

                orderItemViewModel = new OrderItemViewModel
                {
                    ProductList = new List<CartItemDto> { product },
                };
            }
            if(selectedItems.Count != 0)
            {
                orderItemViewModel=  await OrderItemsFromCart(selectedItems);
            }
            
            return View("Index_Order", orderItemViewModel);
           
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<OrderItemViewModel> OrderItemsFromCart( List<OrderTrans> selectedItems)
        {
            
            // Khởi tạo OrderItemViewModel
            var orderItemViewModel = new OrderItemViewModel
            {
                ProductList = new List<CartItemDto>().AsReadOnly(),
            };
            // Tạo danh sách tạm để lưu sản phẩm và số lượng
            var products = new List<CartItemDto>();
            // Xử lý danh sách selectedItems
            foreach (var item in selectedItems)
            {
                Console.WriteLine($"ProductId: {item.ProductId}");
                var product = await _productService.Get_ItemFromCart(item.ProductId);
                if (product != null)
                {
                    // Thêm vào danh sách tạm
                    products.Add(product);
                }
                else
                {
                    Console.WriteLine($"ProductId {item.ProductId} không tồn tại.");
                }
            }
            // Gán danh sách vào OrderItemViewModel
            orderItemViewModel.ProductList = products.AsReadOnly();

            return orderItemViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderSubmissionModel model)
        {
            //var currentUserId = Convert.ToInt32(AbpSession.GetUserId());
            //foreach (var item in model.Items)
            //{
            //    await AddProductToConfirmed(item, currentUserId);
            //}

            return View(model);
        }

        public async System.Threading.Tasks.Task AddProductToConfirmed(OrderItemSubmissionModel item , int currentUserId)
        {
            var addProductToOrderInput = new CartInput()
            {
                idUser = currentUserId,
                idProduct = item.ProductId,
                Quantity = item.Quantity,
                Status = "Confirmed",
            };
            await _productService.AddProductToCart(addProductToOrderInput);
        }

    }
}
