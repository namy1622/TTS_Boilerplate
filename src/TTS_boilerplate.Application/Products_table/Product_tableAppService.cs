using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products_table.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TTS_boilerplate.Products_table
{
   [AbpAuthorize(PermissionNames.Pages_Products)]
    public class Product_tableAppService : TTS_boilerplateAppServiceBase, IProduct_tableAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        public Product_tableAppService(
            IRepository<Product> productRepository,
            IRepository<TTS_boilerplate.Models.Category> categoryRepository,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetCategoryComboboxItem()
        {
            var category = await _categoryRepository.GetAllListAsync();

            return new ListResultDto<ComboboxItemDto>(
                category.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.NameCategory)).ToList());
        }
       
        public async Task<ListResultDto<CategoryDto>> GetCategory()
        {
            var categories = await _categoryRepository.GetAllListAsync();
                
            return new ListResultDto<CategoryDto>(ObjectMapper.Map<List<CategoryDto>>(categories));
        }

        [AbpAuthorize(PermissionNames.Pages_Products)]
        public async Task<PagedResultDto<ProductDto>> GetAllProductCategory(ProductInput input)
        {
            var allProduct = _productRepository.GetAll().Include(t => t.BelongToCategory)
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.NameProduct.Contains(input.Keyword));

            // loc theo ten
            if(!string.IsNullOrEmpty(input.StartDate) && !string.IsNullOrEmpty(input.EndDate))
            {
                var start = Convert.ToDateTime(input.StartDate );
                var end = Convert.ToDateTime(input.EndDate );
                allProduct = allProduct.Where(p => p.CreationDate >= start); // && p.ExpirationDate < end);
                allProduct = allProduct.Where(p => p.ExpirationDate <= end);
            }

            var priceFrom = Convert.ToInt32(input.PriceFrom);
            var priceTo = Convert.ToInt32(input.PriceTo);
            if (priceFrom != 0 || priceTo != 0)
            {
                allProduct = allProduct.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
            }
          
            var totalCount = await allProduct.CountAsync();

            if(input.Sorting != "undefined asc")
            {
                allProduct = allProduct.OrderBy(input.Sorting);
            }
            var products = await allProduct
                                .PageBy(input)
                                .Select(p => new ProductDto
                                {
                                    Id = p.Id,
                                    NameProduct = p.NameProduct,
                                    DescriptionProduct = p.DescriptionProduct,
                                    Price = p.Price,
                                    CreationDate = p.CreationDate,
                                    ExpirationDate = p.ExpirationDate,

                                    ProductImagePath = p.ProductImagePath,

                                    CategoryId = p.CategoryId ?? 0,
                                    NameCategory = p.BelongToCategory != null ? p.BelongToCategory.NameCategory : string.Empty
                                }).ToListAsync();

            return new PagedResultDto<ProductDto> { Items = products, TotalCount = totalCount}; 
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id);
           

            return new ProductDto
            {
                Id = product.Id,
                NameProduct = product.NameProduct,
                DescriptionProduct = product.DescriptionProduct,
                Price = product.Price,
                CreationDate = product.CreationDate,
                ExpirationDate = product.ExpirationDate,
                CategoryId = product.CategoryId ?? 0,
                NameCategory = product.BelongToCategory != null ? product.BelongToCategory.NameCategory : string.Empty,
                ProductImagePath = product.ProductImagePath != null ? product.ProductImagePath.ToString() : null
            };
        }


        public async System.Threading.Tasks.Task CreateProduct(CreateInput input)
        {
            Logger.Info("CreateProduct called");
            string imagePath = null;

            if (input.ProductImagePath != null && input.ProductImagePath.Length > 0)
            {
                imagePath = await SaveImageAsync(input.ProductImagePath);
            }
            try
            {
                var product = new Product
                {
                    NameProduct = input.NameProduct,
                    DescriptionProduct = input.DescriptionProduct,
                    CreationDate = input.CreationDate,
                    ExpirationDate = input.ExpirationDate,
                    Price = input.Price,
                    CategoryId = input.CategoryId,
                    ProductImagePath = imagePath
                };
                await _productRepository.InsertAsync(product);
            }
            catch(Exception e)
            {
                throw new UserFriendlyException("--- Service:" + e);
            }  
        }

        public async System.Threading.Tasks.Task UpdateProduct(UpdateProductDto input)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
          
            if (product == null)
            {
                throw new UserFriendlyException($"Không tìm thấy sản phẩm! + {Convert.ToInt32(input.Id)}");
            }

            // Cập nhật các thuộc tính
            product.NameProduct = input.NameProduct;
            product.DescriptionProduct = input.DescriptionProduct;
            product.Price = input.Price;
            product.CreationDate = input.CreationDate;
            product.ExpirationDate = input.ExpirationDate;
            product.CategoryId = input.CategoryId != 0 ? input.CategoryId : 0;

            if(input.ProductImagePath != null)
            {
                product.ProductImagePath = await SaveImageAsync(input.ProductImagePath);
            }else
            {
                product.ProductImagePath = input.ExistingImageUrl;
            }
            await _productRepository.UpdateAsync(product);
        }

        public async System.Threading.Tasks.Task DeleteProduct(int id)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new UserFriendlyException($"Không tìm thấy sản phẩm với ID: {id}");
            }
            await _productRepository.DeleteAsync(product);
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            // Đường dẫn thư mục lưu ảnh: wwwroot/uploads/products
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Tạo tên file duy nhất
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            // Lưu file ảnh vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            // Lưu đường dẫn tương đối vào database
            //return $"/images/products/{fileName}"; // Trả về đường dẫn để lưu vào database


            // Lấy base URL từ configuration
            var baseUrl = _configuration["App:BaseUrl"];
            if (string.IsNullOrEmpty((string)baseUrl))
            {
                throw new UserFriendlyException("BaseUrl is not configured in appsettings.json");
            }

            // Trả về đường dẫn tuyệt đối
            return $"{baseUrl}/images/products/{fileName}";
           
        }
    }
}


//.PageBy(input) làm gì?

//Nó tương đương với đoạn này:

//query.Skip(input.SkipCount).Take(input.MaxResultCount);

//Vì PagedAndSortedResultRequestDto đã có sẵn 2 thuộc tính:

//public int SkipCount { get; set; }       // Số dòng cần bỏ qua (phân trang)
//public int MaxResultCount { get; set; }  // Số lượng dòng cần lấy (kích thước 1 tran