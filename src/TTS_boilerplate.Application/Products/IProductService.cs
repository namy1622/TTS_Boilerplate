using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Products.Dto;

namespace TTS_boilerplate.Products
{
    public interface IProductService : IApplicationService
    {
        Task<ListResultDto<ProductListDto>> GetAll_Product(); // (GetAll_ProductIntput productInput);

        Task<ProductListDto> GetProduct(int id);

        Task<ListResultDto<CategoryDto>> GetCategory();

       


    }
}
