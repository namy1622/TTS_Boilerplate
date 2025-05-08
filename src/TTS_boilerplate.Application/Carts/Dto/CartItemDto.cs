using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Carts.Dto
{
    public class CartItemDto
    {
        public int Id { set; get; }
        public int CartId { set; get; }
        public int ProductId { set; get; }
        public string NameProduct { set; get; }
        
        public decimal Price { set; get; }
        public int Quantity { set; get; } = 1;

        public string DescriptionProduct { set; get; }
        public string ProductImagePath { get; set; }
        
        public decimal TotalPrice()
        {
          return Price * Quantity;
        }

  }
}
