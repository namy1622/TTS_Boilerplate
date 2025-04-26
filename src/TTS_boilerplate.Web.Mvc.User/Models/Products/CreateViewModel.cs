using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TTS_boilerplate.Web.Models.Products
{
    public class CreateViewModel
    {
        public List<SelectListItem> Category {  get; set; }

        public CreateViewModel ( List<SelectListItem> category)
        {
            this.Category = category;
        }

       
    }
}
