using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.Core;
using TTS_boilerplate.Models;
using TTS_boilerplate.Orders.Dto;

namespace TTS_boilerplate.Orders
{
    public class OrdersAppService : IOrdersAppService
    {
      private readonly IRepository<User, long> _userRepository;
      private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;

      public OrdersAppService(
          IRepository<User, long> userRepository,
          IRepository<Product> productRepository,
          IRepository<Cart> cartRepository,
          IRepository<CartItem> cartItemRepository)
        
        {
        _userRepository = userRepository;
        _productRepository = productRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

   

        public async System.Threading.Tasks.Task AddOrderItem_FromHomeToOrder(OrderItemInput input)
    {
      //  var orderItem = new OrderItemDto
      //  {
      //    OrderId1 = input.OrderId,
      //    ProductId = input.ProductId,
      //    //ProductName = input.NameProduct,
      //    UnitPrice = input.UnitPrice,
      //    Quantity = input.Quantity,
      //    OrderId = input.OrderId,
      //    //DescriptionProduct = input.DescriptionProduct,
      //    //ProductImagePath = input.ProductImagePath
      //  };
       

      //try
      //{
      //  await _orderItemRepository.InsertAsync(new OrderItem
      //  {
         
      //    OrderId = orderItem.OrderId,
      //    OrderId1 = orderItem.OrderId1,
      //    ProductId = orderItem.ProductId,
      //    //ProductName = orderItem.ProductName,
      //    UnitPrice = orderItem.UnitPrice,
      //    Quantity = orderItem.Quantity,

      //  });
      //}
      //catch (Exception ex)
      //{
      //  Console.WriteLine($"❌ Error inserting OrderItem: {ex.Message}");
      //  throw;
      //}
    }

        public async Task<OrderItemDto> GetItemOrder(int idProduct)
        {
            var product =  _cartItemRepository.FirstOrDefault(c => c.ProductId1 == idProduct);

            var productDto = new OrderItemDto
            {
                Quantity = product.Quantity
            };

            return productDto;
        }

        public async System.Threading.Tasks.Task InitOrder(OrderInput input)
    {
        var cart = new Cart
        {
          UserId = input.UserId,
          //OrderDate =input.NowDate,
        };
        await _cartRepository.InsertAsync(cart);
      }   
  }
    
}

