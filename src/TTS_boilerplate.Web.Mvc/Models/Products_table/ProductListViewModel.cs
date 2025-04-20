using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Products_table.Dto;

namespace TTS_boilerplate.Web.Models.Product_table
{
    public class ProductListViewModel 
    {
      public IReadOnlyList<ProductDto> ProductList { set; get; }

        public IReadOnlyList<CategoryDto> CategoryList { set; get; }

        //public ProductListViewModel(IReadOnlyList<ProductDto> productList)
        //{
        //    ProductList = productList;
        //}
    }
}
