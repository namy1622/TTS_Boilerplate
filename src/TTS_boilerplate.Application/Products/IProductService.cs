using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Products
{
    public interface IProductService : IApplicationService
    {
        Task<PagedResultDto<ProductListDto>> GetAll_Product(InputProduct input);//'(ProductInput input ); // (GetAll_ProductIntput productInput);

        Task<ProductListDto> GetProduct(int? id);

        Task<ListResultDto<CategoryDto>> GetCategory();

        //Task AddProductToCart(int idProduct, int idUser);
        Task AddProductToCart(CartInput input);

        Task<PagedResultDto<CartItemDto>> Get_ListCartItem(int userId);

        System.Threading.Tasks.Task InitCart(int userId);
        Task<CartItemDto> Get_CartItem(int? productId);

        

    }
}
