using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Products
{
    public class ProductService : TTS_boilerplateAppServiceBase, IProductService
    {
        private readonly IRepository<Product> _productRepository; // repo lưu trữ để trương tác với Entity product trong db 
        private readonly ILookupAppService _lookupAppService;
        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;

        private readonly ILogger log;
        public ProductService(
            IRepository<Product> productRepository, 
            ILookupAppService lookupAppService, ILogger _log,
            IRepository<TTS_boilerplate.Models.Category> categoryRepository)
        {
            _productRepository = productRepository;
            _lookupAppService = lookupAppService;
            _categoryRepository = categoryRepository;
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

        public async Task<ListResultDto<CategoryDto>> GetCategory()
        {
            var categories = await _categoryRepository.GetAll()
                                .Select(c => new CategoryDto
                                {
                                    NameCategory = c.NameCategory,
                                    Id = c.Id
                                }).ToListAsync();

            return new ListResultDto<CategoryDto>
            {
                Items = categories
            };
                
        }

        public async Task<ProductListDto> GetProduct(int id)
        {
            log.Info($"Bắt đầu truy vấn sản phẩm với ID: {id}");
            try
            {
                // Có thể xảy ra nhiều loại lỗi khác nhau
                var product = await _productRepository
                    .GetAllIncluding(p => p.BelongToCategory)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    throw new UserFriendlyException(L("ProductNotFound", id));
                }

                return ObjectMapper.Map<ProductListDto>(product);
            }
            catch (DbException ex) // Lỗi database
            {
                log.Error($"Lỗi database: {ex.Message}", ex);
                throw new UserFriendlyException(L("DatabaseError"));
            }
            catch (Exception ex) // Các lỗi khác
            {
                log.Error($"Lỗi không xác định: {ex.Message}", ex);
                throw new UserFriendlyException(L("UnknownError"));
            }
        }

        //public async System.Threading.Tasks.Task Update(ProductInput input)
        //{
        //    //var product = new Product
        //    //{
        //    //    NameProduct = input.NameProduct,
        //    //    DescriptionProduct = input.DescriptionProduct,
        //    //    Price = input.Price,
        //    //    CreationDate = input.CreationDate,
        //    //    ExpirationDate = input.ExpirationDate
        //    //};
        //    //_productRepository.UpdateAsync((product));

        //    Console.WriteLine($"🔍 Kiểm tra ID nhận được: {input.Id}");

        //    var product = await _productRepository.FirstOrDefaultAsync(input.Id);

        //    Console.Write(product);

        //    if (product == null)
        //    {
        //        throw new UserFriendlyException($"Không tìm thấy sản phẩm! + {Convert.ToInt32(input.Id)}");
        //    }

        //    // Cập nhật các thuộc tính
        //    product.NameProduct = input.NameProduct;
        //    product.DescriptionProduct = input.DescriptionProduct;
        //    product.Price = input.Price;
        //    product.CreationDate = input.CreationDate;
        //    product.ExpirationDate = input.ExpirationDate;

        //    await _productRepository.UpdateAsync(product);
        //}

        //public async System.Threading.Tasks.Task Create(ProductInput input)
        //{
        //    try
        //    {
        //        //Cach 1:(phải automapTo(Product) ở ProductInput, ProductListDto
        //        var product = ObjectMapper.Map<Product>(input);


        //        //Cach 2:
        //        //var product = new Product
        //        //{
        //        //    NameProduct = input.NameProduct,
        //        //    DescriptionProduct = input.DescriptionProduct,
        //        //    Price = input.Price,
        //        //    CreationDate = input.CreationDate,
        //        //    ExpirationDate = input.ExpirationDate,
        //        //    //CategoryId = null
        //        //};

        //        Console.Write("fsdf");
        //        await _productRepository.InsertAsync(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Lỗi khi tạo sản phẩm", ex);
        //        throw new UserFriendlyException("Có lỗi xảy ra khi tạo sản phẩm, vui lòng thử lại!");

                
        //    }
            
        //}

        //public async System.Threading.Tasks.Task Delete(int id)
        //{
        //    await _productRepository.DeleteAsync(id);
        //}

       
    }
}

//_$productList.on('click', '.card__product', function () {
//            const productId = $(this).data('product-id');
//            $.ajax({
//url: abp.appPath + 'Products/GetProductById?id=' + productId,
//                method: 'GET',
//                success: function(product) {
//                    $('#modalName').text(product.nameProduct);
//                    $('#modalImage').attr('src', product.productImagePath);
//                    $('#modalDescription').text('Mô tả: ' + (product.descriptionProduct || 'Không có mô tả'));
//                    $('#modalPrice').text(product.price + ' VND');
//                    $('#productModal').modal('show');
//    },
//                error: function(e) {
//        console.error('--error--', e);
//        alert('Lỗi khi tải chi tiết sản phẩm.');
//    }
//});
//        });