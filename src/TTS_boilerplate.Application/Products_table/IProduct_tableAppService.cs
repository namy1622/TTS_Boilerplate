using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Products_table.Dto;
using TTS_boilerplate.Roles.Dto;

namespace TTS_boilerplate.Products_table
{
    public interface IProduct_tableAppService : IApplicationService
    {
        Task<ListResultDto<CategoryDto>> GetCategory();

        Task<PagedResultDto<ProductDto>> GetAllProductCategory(ProductInput input);

        Task<ProductDto> GetProduct(int id);

        Task DeleteProduct(int id);

        Task CreateProduct(CreateInput inputProduct);

        Task UpdateProduct(UpdateProductDto inputProduct);

        Task<ListResultDto<ComboboxItemDto>> GetCategoryComboboxItem();
    }
}
