using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.ExportFile;
using TTS_boilerplate.Products.Dto;
using TTS_boilerplate.Products_table;
using TTS_boilerplate.Products_table.Dto;
using TTS_boilerplate.Web.Models.Product_table;
using TTS_boilerplate.Web.Models.Products_table;
using ProductInput = TTS_boilerplate.Products.Dto.ProductInput;

namespace TTS_boilerplate.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Products)]
    public class Products_tableController : TTS_boilerplateControllerBase
    {
        private readonly IProduct_tableAppService _product_TableAppService;
        private readonly IExportFileAppService _exportFileAppService;

        public Products_tableController(IProduct_tableAppService product_TableAppService, IExportFileAppService exportFileAppService)
        {
           _product_TableAppService = product_TableAppService;
            _exportFileAppService = exportFileAppService;
        }
        [AbpAuthorize(PermissionNames.Products_R)]        
        public async Task<ActionResult> Index()
        {
            var categories = (await _product_TableAppService.GetCategory()).Items;

            var model = new ProductListViewModel
            {
                CategoryList = categories
            };
            //return View();
            return View(model);
        }

        [AbpAuthorize(PermissionNames.Products_U)]
        public async Task<ActionResult> EditProduct(int productId)
        {
            var product = await _product_TableAppService.GetProduct(productId);
            var categories = (await _product_TableAppService.GetCategory()).Items;
            var model = new EditProductViewModel
            {
                product = product,
                categories = categories
                //.Select(c => new SelectListItem
                //{
                //    Value = c.Id.ToString(),
                //    Text = c.NameCategory,
                //    Selected = c.Id == product.CategoryId
                //}).ToList()
            };
            return PartialView("_EditProduct", model);
        }

        [AbpAuthorize(PermissionNames.Products_U)]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateProductDto input)
        {

            // Đảm bảo giá trị Price được parse đúng
            if (input.Price.HasValue)
            {
                
                input.Price = Math.Round(input.Price.Value, 2); // Làm tròn 2 chữ số thập phân
            }
            await _product_TableAppService.UpdateProduct(input);
            return Ok();
        }

        [AbpAuthorize(PermissionNames.Products_C)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateInput input)
        {
            try
            {
                await _product_TableAppService.CreateProduct(input);
                return Ok(new { Message = "Product created successfully" });
            }
            catch (UserFriendlyException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
            }
        }

        [HttpGet]
        public async Task<FileResult> ExportToExcel()
        {
            var data = await _product_TableAppService.GetAllProduct_exportExcel();

            var excelBytes = await _exportFileAppService.ExportToExcel(data, "Products");

            return new FileContentResult(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "Products.xlsx"
            };
        }

    }
}
