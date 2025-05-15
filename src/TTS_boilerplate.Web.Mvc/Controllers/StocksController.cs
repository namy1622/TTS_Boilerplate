using Microsoft.AspNetCore.Mvc;
using TTS_boilerplate.Controllers;

namespace TTS_boilerplate.Web.Controllers
{
    public class StocksController : TTS_boilerplateControllerBase
    {
        public IActionResult Index_Stocks()
        {
            return View();
        }
    }
}
