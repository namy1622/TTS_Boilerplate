using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Core;

namespace TTS_boilerplate.Orders.Dto
{
  public class OrderItemDto
  {
       
        public int Id { set; get; }
        public int CartId { set; get; }
        public int ProductId { set; get; }
        public string NameProduct { set; get; }

        public decimal Price { set; get; }
        public int Quantity { set; get; }

        public string DescriptionProduct { set; get; }
        public string ProductImagePath { get; set; }

        public decimal TotalPrice()
        {
            return Price * Quantity;
        }
        //  public Guid OrderId { set; get; }
        //  public int ProductId { set; get; }
        //  public string? ProductName { set; get; }

        //  public decimal? UnitPrice { set; get; }
        //  public int? Quantity { set; get; }

        //  public string? DescriptionProduct { set; get; }
        //  public string? ProductImagePath { get; set; }
        //public Guid OrderId1 { get; set; }

    }
}
