using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products.Dto;
using TTS_boilerplate.Products_table.Dto;


namespace TTS_boilerplate.Category
{
    public class CategoryAppService : TTS_boilerplateAppServiceBase, ICategoryAppService
    {
       

        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;

        private readonly ILogger _logger;

        public CategoryAppService(
           
             IRepository<TTS_boilerplate.Models.Category> categoryRepository,
              ILogger logger,
              IRepository<Product> productRepository)
        {
           
            _categoryRepository = categoryRepository;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<PagedResultDto<CategoryDto>> GetAllCategory(CategoryInput input)
        {
            var allCategory = await _categoryRepository.GetAllAsync();

            var totalCount = await  allCategory.CountAsync();

            var productCounts = await _productRepository.GetAll()
                   .GroupBy(p => p.CategoryId)
                   .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                   .ToDictionaryAsync(g => g.CategoryId, g => g.Count); // chuyển result thành Dictionary<int, int> (key: CategoryId, value: count)

            var categories =  allCategory
                .OrderBy(input.Sorting)
                .PageBy(input)
                .Select(p => new CategoryDto
                {
                    Id = p.Id,
                    NameCategory = p.NameCategory,
                    TotalProduct = (productCounts.ContainsKey(p.Id) ? productCounts[p.Id] : 0).ToString()
                }).ToList();

            Console.WriteLine($"-- categories + {categories}");
            _logger.Error($"-- categories + {categories}");
            return new PagedResultDto<CategoryDto>
            {
                Items = categories,
                TotalCount = totalCount
            };
        }

        public async System.Threading.Tasks.Task CreateCategory(CategoryInput inpput)
        {
            var category = new TTS_boilerplate.Models.Category
            {
                NameCategory = inpput.NameCategory,
            };

            await _categoryRepository.InsertAsync(category);
        }

        public async System.Threading.Tasks.Task DeleteCategory(int id)
        {
            #region cach1: neu category đang có trong product thì ko cho xóa 
            // Kiểm tra xem có Product nào đang sử dụng Category này không
            //bool containCategory = await _productRepository.CountAsync(p => p.CategoryId == id) > 0;
            //if (containCategory)
            //{
            //    throw new UserFriendlyException("Không thể xóa Category do đang được sử dụng!");
            //}
            #endregion

            #region cach2: Xóa luôn Product có category bị xóa 
            //var relatedProducts = await _productRepository.GetAllListAsync(p => p.CategoryId == id);

            //foreach(var product in relatedProducts)
            //{
            //    _productRepository.DeleteAsync(product);
            //}
            #endregion

            #region cach3: set Null categoryId trong Porduct

            var relatedProduct = await _productRepository.GetAllListAsync(p => p.CategoryId == id);

            foreach(var product in relatedProduct)
            {
                product.CategoryId = null;
                await _productRepository.UpdateAsync(product);
            }

            #endregion

            await _categoryRepository.DeleteAsync(id);
        }
   
        public async System.Threading.Tasks.Task Updatecategory(CategoryDto input)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(c => c.Id == input.Id);

            if(category == null)
            {
                throw new UserFriendlyException("Không tim thấy sản phẩm có tên " + input.NameCategory);
            }

            category.NameCategory = input.NameCategory;

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new UserFriendlyException("Không tìm thấy sản phẩm");
            }
            return new CategoryDto
            {
                Id = category.Id,
                NameCategory = category.NameCategory
            };
        }
    }
}
