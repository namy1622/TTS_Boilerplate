using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization.Users;

namespace TTS_boilerplate.Models
{
    public class DiscountOfUser : Entity
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
