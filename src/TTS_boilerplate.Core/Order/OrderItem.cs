using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Core
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; set; } //
        public int ProductId { get; set; } //
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; } // giá sp tại thời điểm đặt hàng 
        public int Quantity { get; set; } // so luong sp

        public OrderItem() { }

        public OrderItem(Guid orderId, int productId, string productName, decimal unitPrice, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
