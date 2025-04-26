using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Core
{
    //public class Cart : Entity<Guid>
    public class Cart : Entity
    {
        public long? UserId { get; set; } // 
        public List<CartItem> CartItems { get; set; } // ds sanpham trong gio hang

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

       
    }
}
