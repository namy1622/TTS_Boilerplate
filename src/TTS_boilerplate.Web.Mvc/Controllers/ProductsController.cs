using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Products;
using TTS_boilerplate.Web.Models.Products;

namespace TTS_boilerplate.Web.Controllers
{
    //[AbpAuthorize(PermissionNames.Pages_Products)]
    public class ProductsController : TTS_boilerplateControllerBase
    {
        private readonly IProductService _productService;

        private readonly ILookupAppService _lookupAppService;

        public ProductsController(IProductService productService, ILookupAppService lookupAppService)
        {
            _productService = productService;

            _lookupAppService = lookupAppService;
        }

        public async Task<ActionResult> Index()
    {
      //var allProduct = await _productService.GetAll_Product();
      var allCategories = await _productService.GetCategory(); // Lấy danh sách categories

      var categorySelectListItems = (await _lookupAppService.GetCategoryComboboxItem()).Items
         .Select(c => c.ToSelectListItem())
         .ToList();
      //var allCategory = await _productService.Get();
      //var model = new IndexViewModel(allProduct.Items, categorySelectListItems);
      var model = new IndexViewModel(categorySelectListItems);
      ViewBag.Categories = allCategories.Items;
      return View(model);
      //return Ok();
      //return Ok();

    }

    public async Task<ActionResult> Create()
        {

            var categorySelectListItems = (await _lookupAppService.GetCategoryComboboxItem()).Items
                .Select(c => c.ToSelectListItem())
                .ToList();
            return View(new CreateViewModel(categorySelectListItems));
        }
    }
}