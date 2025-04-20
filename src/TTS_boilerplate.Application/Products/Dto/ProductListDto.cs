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
    [AutoMapFrom(typeof(Product))]
    [AutoMapTo(typeof(Product))]
    public class ProductListDto : EntityDto
    {
        public int Id { set; get; }
        
        public string NameProduct { set; get; }

        
        public string DescriptionProduct { set; get; }

        public Decimal? Price { set; get; }

        public DateTime CreationDate { set; get; }

        public DateTime ExpirationDate { set; get; }
        //public Date CreationDate {set; get;}

        //public Date ExpirationDate { set; get; }

        public string ProductImagePath { get; set; }

        public int CategoryId { set; get; }

        public string NameCategory { set; get; }

    }
}
