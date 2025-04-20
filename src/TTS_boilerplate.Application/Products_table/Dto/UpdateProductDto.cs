using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Products_table.Dto
{
    public class UpdateProductDto
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

        public IFormFile ProductImagePath { get; set; }

        public string ExistingImageUrl { get; set; } // Lưu ảnh cũ

        [Required]
        public DateTime CreationDate { get; set; }

        //[DisplayName("Ngày hết hạn")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        public int CategoryId { set; get; }

        public string NameCategory { set; get; }
    }
}
