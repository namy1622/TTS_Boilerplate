using System.Collections.Generic;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Orders.Dto;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Web.Models.Orders
{
    public class OrderItemViewModel
    {
        public ProductListDto Product { get; set; }
        public OrderItemDto Quantity { get; set; }

        public IReadOnlyList<ProductListDto> ProductList { get; set; }

        public IReadOnlyList<OrderItemDto> CartList { get; set; }
    }
}
