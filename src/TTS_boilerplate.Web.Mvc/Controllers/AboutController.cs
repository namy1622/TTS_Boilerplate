using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using TTS_boilerplate.Controllers;

namespace TTS_boilerplate.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : TTS_boilerplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
