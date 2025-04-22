using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Category.Dto
{
    [AutoMap(typeof(TTS_boilerplate.Models.Category))]
    public class CategoryDto :Entity
    {
        public int Id { set; get; }
        [Required]
        [StringLength(32)]
        public string NameCategory { set; get; }

        public string TotalProduct { set; get; }
    }
}
