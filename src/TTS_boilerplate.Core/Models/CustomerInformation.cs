using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization.Users;

namespace TTS_boilerplate.Models
{
    public class CustomerInformation 
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
