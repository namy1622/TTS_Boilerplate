using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TTS_boilerplate.Products.Dto
{
    [AutoMapTo(typeof(Product))]
    public class ProductInput: PagedAndSortedResultRequestDto
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(Product.MaxTitleLength)]
        public string? NameProduct { set; get; }

        [StringLength(Product.MaxDescriptionLength)]
        public string? DescriptionProduct { set; get; }

        public decimal? Price { set; get; }

        public DateTime? CreationDate { set; get; }

        public DateTime? ExpirationDate { set; get; }

        //public Date CreationDate { set; get; }

        //public Date ExpirationDate { set; get; }


        //public IFormFile ProductImage { get; set; }

        public int? CategoryId { get; set; }

        public int? userId { get; set; }
    }
}
