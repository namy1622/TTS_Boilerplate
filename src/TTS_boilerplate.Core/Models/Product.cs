﻿using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTS_boilerplate.Models
{
    [Table("AppProducts")]
    public class Product : Entity
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB


        [Required]
        [StringLength(MaxTitleLength)]
        public string NameProduct { set; get; }

        public string DescriptionProduct { set; get; }

        [Precision(18, 2)]
        public decimal? Price { set; get; }

        public int? QuantityInStock { set; get; }

        public int? SoldQuantity { get; set; }

     
        [DisplayName("Ngày sản xuất")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Ngày hết hạn")]
        public DateTime ExpirationDate { get; set; } 

        [StringLength(512)] // độ dài đường dẫn ảnh
        public string ProductImagePath { get; set; } // đường dẫn ảnh


        [ForeignKey(nameof(CategoryId))]
        public Category BelongToCategory { get; set; }
        public int? CategoryId { get; set; }



        public Product()
        {

        }

       
    }


}
