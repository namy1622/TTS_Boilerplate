using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;

namespace TTS_boilerplate.Products_table.Dto
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<CategoryDto, TTS_boilerplate.Models.Category>();
        }

        
    }
}
