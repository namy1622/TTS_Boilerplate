using System.Collections.Generic;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products_table.Dto;

namespace TTS_boilerplate.Web.Models.Products_table
{
    public class EditProductViewModel
    {
        public ProductDto product {  get; set; }

        public IReadOnlyList<CategoryDto> categories { get; set; }
    }
}
