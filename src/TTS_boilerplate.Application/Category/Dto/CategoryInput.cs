using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Category.Dto
{
    public class CategoryInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {
        //public string Keyword { get; set; }

        //public string Sorting { get; set; }

        //public int? CategoryId { get; set; }
        //[Range(0, int.MaxValue)]
        //public  override int SkipCount { get; set; } = 5;

        //[Range(1, int.MaxValue)]
        //public override int MaxResultCount { get; set; } = 5;

        public string NameCategory { get; set; }
    
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id asc";
            }
        }
    }
}
