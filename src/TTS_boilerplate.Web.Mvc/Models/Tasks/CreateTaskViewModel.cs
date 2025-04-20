using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TTS_boilerplate.Web.Models.Tasks
{
    public class CreateTaskViewModel
    {
        public List<SelectListItem> People { set; get; }

        public CreateTaskViewModel(List<SelectListItem> people)
        {
            People = people;
        }
    }
}
