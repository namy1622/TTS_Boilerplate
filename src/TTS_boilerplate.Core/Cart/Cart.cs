using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Core
{
    public class Cart : Entity<Guid>
    {
        public long? UserId { get; set; } // 
        public List<CartItem> CartItems { get; set; } // ds sanpham trong gio hang

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        //
        public void AddItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            var existingItem = CartItems.Find(item => item.ProductId == productId);

            if (existingItem != null)  // đã có sản phẩm trong giỏ
            {
                existingItem.Quantity += quantity; // tăng số lượng 
            }
            else
            {
                CartItems.Add(new CartItem(
                    productId,
                    productName,
                    unitPrice,
                    quantity
                ));
            }

        }
        public void RemoveItem(int productId)
        {
            CartItems.RemoveAll(item => item.ProductId == productId);
        }

        //
        public decimal CaculateTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in CartItems)
            {
                totalAmount += item.UnitPrice * item.Quantity;
            }
            return totalAmount;
        }
    }
}
