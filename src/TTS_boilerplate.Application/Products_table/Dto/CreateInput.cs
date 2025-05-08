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
    public class CreateInput
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB
        [Required]
        [StringLength(MaxTitleLength)]
        public string NameProduct { set; get; }

        [Required]
        [StringLength(MaxDescriptionLength)]
        public string DescriptionProduct { set; get; }

        [Precision(18,2)]
        public Decimal? Price { set; get; }
        public int? Stock { set; get; }
        public DateTime CreationDate { set; get; }

        public DateTime ExpirationDate { set; get; }
        //public Date CreationDate { set; get; }

        //public Date ExpirationDate { set; get; }

        public IFormFile ProductImagePath { get; set; }

        public int CategoryId { set; get; }

        public string NameCategory { set; get; }
    }
}
