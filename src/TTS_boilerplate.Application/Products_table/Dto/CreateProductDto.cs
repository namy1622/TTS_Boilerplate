using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TTS_boilerplate.Products_table.Dto
{
    [AutoMapTo(typeof(Product))]
    public class CreateProductDto
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        
        public int Id { set; get; }

        [Required]
        [StringLength(MaxTitleLength)]
        public string NameProduct { set; get; }

        [Required]
        public string DescriptionProduct { set; get; }

        [Required]
        [Precision(18, 2)]
        public decimal? Price { set; get; }

        //[DisplayName("Ngày sản xuất")]
        //[Required]
        //public Date CreationDate { get; set; }

        ////[DisplayName("Ngày hết hạn")]
        //[Required]
        //public Date ExpirationDate { get; set; }//[DisplayName("Ngày sản xuất")]
        [Required]
        public DateTime CreationDate { get; set; }

        //[DisplayName("Ngày hết hạn")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        public IFormFile ProductImagePath { get; set; } // thêm ảnh

        public int CategoryId { set; get; }

        public string NameCategory { set; get; }

    }
}
