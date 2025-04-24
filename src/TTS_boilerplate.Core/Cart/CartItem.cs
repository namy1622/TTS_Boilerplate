using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;

namespace TTS_boilerplate.Core
{
    public class CartItem :Entity
    {

        public int CartId { get; set; } // id gio hang
        
        //public DateTime OrderDate { get; set; }

        //public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } // so luong sp

        [ForeignKey("ProductId1")]
        public  Product? Product { get; set; }
        // virtual để Entity Framework để có thể lazy load relationship
        public int ProductId { get; set; }
        public int ProductId1 { get; set; }

        public CartItem() { }

        //public CartItem(int cartId, int productId, int quanity)
        //{
        //    CartId = cartId;
        //    ProductId = productId;
        //    Quantity = quanity;
        //}
    }
}
