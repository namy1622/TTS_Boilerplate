using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Models
{
    [Table("AppCategory")]
    public class Category : Entity<int>
    {
        public const int MaxNameLength = 32;

        [Required]
        [StringLength(MaxNameLength)]
        public string NameCategory { set; get; }

        public Category() { }

        public Category(string nameCategory)
        {
            NameCategory = nameCategory;
        }


    }
}
