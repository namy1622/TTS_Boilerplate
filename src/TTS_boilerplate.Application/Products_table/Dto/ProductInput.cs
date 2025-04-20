using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Products_table.Dto
{
    public class ProductInput : PagedAndSortedResultRequestDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public string NameFilter { get; set; }
        public string PriceFilter { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string PriceFrom { set; get; }
        public string PriceTo { set; get; }
        //public override int SkipCount { get => base.SkipCount; set => base.SkipCount = 5; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting) )
            {
                Sorting = "NameProduct desc";// desc";
            }
        }

        
    }
    
}
