using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Products
{
    public class ProductService : TTS_boilerplateAppServiceBase, IProductService
    {
        private readonly IRepository<Product> _productRepository; // repo lưu trữ để trương tác với Entity product trong db 
        private readonly ILookupAppService _lookupAppService;

        private readonly ILogger log;
        public ProductService(IRepository<Product> productRepository, ILookupAppService lookupAppService, ILogger _log)
        {
            _productRepository = productRepository;
            _lookupAppService = lookupAppService;

            log = _log;
        }

        public async Task<ListResultDto<ProductListDto>> GetAll_Product()
        {
            var allProduct = await _productRepository
                             .GetAll()
                             .Include(t => t.BelongToCategory)
                             
                             .Select(p => new ProductListDto
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
                             })
                             .ToListAsync();

            return new ListResultDto<ProductListDto>(
                ObjectMapper.Map<List<ProductListDto>>(allProduct)) ;
        }

        public async Task<ProductListDto> GetProduct(int id)
        {
            log.Warn("log product.1" + id );
            var product = await  _productRepository.FirstOrDefaultAsync(p => p.Id == id);

            log.Error("log product" + product.ToString());
            Console.WriteLine($"product {product}");
            if (product == null)
            {
                log.Error("Không tìm thấy sản phẩm với ID: " + id);
                throw new UserFriendlyException($"Không tìm thấy sản phẩm với ID: {id}");
            }
            log.Error("log product" + product.ToString());
            return ObjectMapper.Map<ProductListDto>(product);
        }

        public async System.Threading.Tasks.Task Update(ProductInput input)
        {
            //var product = new Product
            //{
            //    NameProduct = input.NameProduct,
            //    DescriptionProduct = input.DescriptionProduct,
            //    Price = input.Price,
            //    CreationDate = input.CreationDate,
            //    ExpirationDate = input.ExpirationDate
            //};
            //_productRepository.UpdateAsync((product));

            Console.WriteLine($"🔍 Kiểm tra ID nhận được: {input.Id}");

            var product = await _productRepository.FirstOrDefaultAsync(input.Id);

            Console.Write(product);

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

            await _productRepository.UpdateAsync(product);
        }

        public async System.Threading.Tasks.Task Create(ProductInput input)
        {
            try
            {
                //Cach 1:(phải automapTo(Product) ở ProductInput, ProductListDto
                var product = ObjectMapper.Map<Product>(input);


                //Cach 2:
                //var product = new Product
                //{
                //    NameProduct = input.NameProduct,
                //    DescriptionProduct = input.DescriptionProduct,
                //    Price = input.Price,
                //    CreationDate = input.CreationDate,
                //    ExpirationDate = input.ExpirationDate,
                //    //CategoryId = null
                //};

                Console.Write("fsdf");
                await _productRepository.InsertAsync(product);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tạo sản phẩm", ex);
                throw new UserFriendlyException("Có lỗi xảy ra khi tạo sản phẩm, vui lòng thử lại!");

                
            }
            
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

       
    }
}