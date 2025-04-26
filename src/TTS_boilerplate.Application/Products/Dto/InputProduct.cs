using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Products.Dto
{
    public class InputProduct: PagedAndSortedResultRequestDto
    {
    public string?[] Categories { get; set; } 
      public int? MinPrice { get; set; }
      public int? MaxPrice { get; set; }
    public string? KeySearch { get; set; } = "Chuột";
  }
}
