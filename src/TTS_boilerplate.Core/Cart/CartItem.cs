using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Core
{
    public class CartItem :Entity<Guid>
    {
        public Guid CartId { get; set; } // id gio hang
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } // so luong sp

        public CartItem() { }

        public CartItem( int productId, string productName, decimal unitPrice, int quanity)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quanity;
        }
    }
}
