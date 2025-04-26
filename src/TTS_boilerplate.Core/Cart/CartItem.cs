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
  public class CartItem : Entity
  {

    public int CartId { get; set; } // id gio hang
    public int Quantity { get; set; } // so luong sp

    [ForeignKey("ProductId1")]
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public int ProductId1 { get; set; }

    public OrderStatus? Status { get; set; } // trạng thái đơn hàng

   

    public enum OrderStatus
    {
      InCart = 0,       // mới thêm vào giỏ hàng
      Pending = 1,      // đã đặt hàng nhưng chưa xác nhận / thanh toán
      Confirmed = 2,    // đã xác nhận đơn hàng
      Shipped = 3,      // đã giao hàng
      Delivered = 4,    // đã nhận hàng
      Canceled = 5      // Khi bị hủy
    }
  }
}
