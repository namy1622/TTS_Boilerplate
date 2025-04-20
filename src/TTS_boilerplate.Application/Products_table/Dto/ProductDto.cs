using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TTS_boilerplate.Products_table.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto<int>
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB
        //[Required]
        [StringLength(MaxTitleLength)]
        public string NameProduct { set; get; }

        //[Required]
        [StringLength(MaxDescriptionLength)]
        public string DescriptionProduct { set; get; }

        [Precision(18, 2)]
        public Decimal? Price { set; get; }

        public DateTime CreationDate { set; get; }

        public DateTime ExpirationDate { set; get; }
 
        public string ProductImagePath { get; set; }

        public int CategoryId { set; get; }

        public string NameCategory { set; get; }

    }
}
