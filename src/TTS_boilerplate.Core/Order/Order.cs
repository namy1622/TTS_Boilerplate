using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Core
{
    public class Order : Entity<Guid>
    {
        public string OrderNumber { get; set; } // mã đơn - là khóa ngoại với Order
        public long? UserId { get; set; }  // id người đặt - là khóa ngoại liên kết với User
        public decimal? TotalAmount { get; set; } // tổng giá trị đơn 
        public OrderStatus? Status { set; get; } //trạng thái đơn hàng 
        public DateTime? OrderDate { get; set; }
        
        public string? NameCustomer { get; set; } // tên người đặt hàng
         public string? PhoneCustomer { get; set; } // số điện thoại người đặt hàng
        public string? AddressCustomer{ get; set; }

        public List<OrderItem> Items { get; set; } //danh sach sản phẩm trong đơn hàng 



        public void CaculateTotalAmount()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.UnitPrice * item.Quantity;
            }
        }

        // Enum để định nghĩa trạng thái đơn hàng 
        public enum OrderStatus
        {
            Pending, //chờ xử lý
            Processing, // đang xử lý
            Shipped, // đã giao hàng
            Delivered, // đã nhận hàng
            Cancelled // đã huỷ 
        }
    }



}
