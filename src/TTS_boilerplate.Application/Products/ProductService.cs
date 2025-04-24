using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Models;
using TTS_boilerplate.Products.Dto;
using Abp.Authorization.Users;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using TTS_boilerplate.Carts.Dto;

namespace TTS_boilerplate.Products
{
    public class ProductService : TTS_boilerplateAppServiceBase, IProductService
    {
        private readonly IRepository<Product> _productRepository; // repo lưu trữ để trương tác với Entity product trong db 
        private readonly ILookupAppService _lookupAppService;
        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;
        private readonly ILogger log;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Cart, int> _cartRepository;
        private readonly IRepository<CartItem, int> _cartItemRepository;
        public ProductService(
            IRepository<Product> productRepository, 
            ILookupAppService lookupAppService, ILogger _log,
            IRepository<TTS_boilerplate.Models.Category> categoryRepository,
            IRepository<User, long> userRepository,
            IRepository<Cart, int> cartRepository,
            IRepository<CartItem, int> cartItemRepository
            )
        {
            _productRepository = productRepository;
            _lookupAppService = lookupAppService;
            _categoryRepository = categoryRepository;
            log = _log;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;

        }

        
        //public async Task<ListResultDto<ProductListDto>> GetAll_Product(ProductInput input)
        //public async Task<ListResultDto<ProductListDto>> GetAll_Product()
        public async Task<PagedResultDto<ProductListDto>> GetAll_Product(InputProduct input)
        {
            var allProduct = await _productRepository
                             .GetAll()
                             .Include(t => t.BelongToCategory)
                             .Skip(input.SkipCount) // Bỏ qua số lượng bản ghi đã được lấy
                             .Take(input.MaxResultCount) // Lấy số lượng bản ghi tối đa

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

            return new PagedResultDto<ProductListDto>
            {
                Items = allProduct,
                TotalCount = _productRepository
                             .GetAll()
                             .Include(t => t.BelongToCategory).Count()
            };
                

//return new PagedResultDto<ProductListDto>(
//                ObjectMapper.Map<List<ProductListDto>>(allProduct)) ;
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

        public async System.Threading.Tasks.Task AddProductToCart(CartInput input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == input.idUser);
            var existPRoduct = await _cartItemRepository.FirstOrDefaultAsync(p => p.ProductId == input.idProduct);
            var existUser = await _cartRepository.FirstOrDefaultAsync(u => u.UserId == input.idUser);
            
            if (existUser == null)
            {
                var cart = new Cart
                {
                    UserId = input.idUser,
                };
                await _cartRepository.InsertAsync(cart);
            }
            if(existPRoduct == null)
            {
                var cartProduct = new CartItem
                {
                    CartId = existUser.Id,
                    ProductId = input.idProduct,
                    Quantity = 2,
                    ProductId1 = input.idProduct
                };
                await _cartItemRepository.InsertAsync(cartProduct);
            }
            else
            { 
                new UserFriendlyException("Đã có sản phẩm trong giỏ!");
            } 
        }

        public async System.Threading.Tasks.Task InitCart(int userId){
            var existUser = await _cartRepository.FirstOrDefaultAsync(u => u.UserId == userId);

            if (existUser == null)
            {
                var cart = new Cart
                {
                    UserId = userId,
                };
                await _cartRepository.InsertAsync(cart);
            }
            else{
                new UserFriendlyException("Tao gio hang that bai (service)");
            }
        }

        public async Task<PagedResultDto<CartItemDto>> Get_ListCartItem(int userId)
        {
            var cartId = await _cartRepository.FirstOrDefaultAsync(c => c.UserId == userId);
            //var cartItem =  _cartItemRepository.GetAllList(cartItem => cartItem.CartId == cartId.Id);
            //cartItem = cartItem.Include(c => c.product);
            

            var cartItem = await _cartItemRepository.GetAll()
                .Include(c => c.Product)
                .Where(c => c.CartId == cartId.Id)
                .ToListAsync();
            //  //var cartItem = _cartItemRepository.Get(c => c.CartId == cartId).

            if (cartItem == null)
            {
                throw new UserFriendlyException(L("CartItemNotFound"));
            }
            return new PagedResultDto<CartItemDto>
            {
                Items = cartItem.Select(c => new CartItemDto
                {
                    Id = c.Id,
                    CartId = c.CartId,
                    ProductId = c.ProductId,
                    NameProduct = c.Product.NameProduct,
                    Price = c.Product.Price ?? 0m,
                    Quantity = c.Quantity,
                    DescriptionProduct = c.Product.DescriptionProduct,
                    ProductImagePath = c.Product.ProductImagePath
                }).ToList(),
                TotalCount = cartItem.Count()
            };
        }

    }
}

