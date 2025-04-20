using Microsoft.AspNetCore.Mvc;
using TTS_boilerplate.Controllers;

namespace TTS_boilerplate.Web.Controllers
{
    public class CartsController : TTS_boilerplateControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
