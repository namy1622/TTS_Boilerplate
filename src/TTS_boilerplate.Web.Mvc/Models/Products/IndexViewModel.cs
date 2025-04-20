using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Web.Models.Products
{
    public class IndexViewModel
    {
        public IReadOnlyList<ProductListDto> ProductList { get; set; }

        public List<SelectListItem> CategoryList { set; get; }

        public IndexViewModel(IReadOnlyList<ProductListDto> productList, List<SelectListItem> categoryList)
        {
            ProductList = productList;
            CategoryList = categoryList;
        }
    }
}
