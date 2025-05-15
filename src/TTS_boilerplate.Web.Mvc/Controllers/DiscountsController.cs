using Microsoft.AspNetCore.Mvc;
using TTS_boilerplate.Controllers;

namespace TTS_boilerplate.Web.Controllers
{
    public class DiscountsController : TTS_boilerplateControllerBase
    {
        public IActionResult Index_Discounts()
        {
            return View();
        }
    }
}
