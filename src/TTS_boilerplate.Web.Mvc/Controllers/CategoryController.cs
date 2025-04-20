using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTS_boilerplate.Authorization;
using TTS_boilerplate.Category;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.Web.Models.Category;

namespace TTS_boilerplate.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Category)]
    public class CategoryController : TTS_boilerplateControllerBase
    {
        private readonly ICategoryAppService _categoryService;

        public CategoryController(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }
        [AbpAuthorize(PermissionNames.Categories_R)]
        public IActionResult Index()
        {
            return View();
        }

        [AbpAuthorize(PermissionNames.Categories_U)]
        public async System.Threading.Tasks.Task<ActionResult> UpdateCategory(int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);

            var model = new CategoryViewModel
            {
                category = category
            };

            return PartialView("Update", model);
        }
    }
}
