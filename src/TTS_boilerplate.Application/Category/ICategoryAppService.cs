using Abp.Application.Services.Dto;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Category.Dto;
using TTS_boilerplate.Products_table.Dto;

namespace TTS_boilerplate.Category
{
    public interface ICategoryAppService : IApplicationBase
    {
        Task<PagedResultDto<CategoryDto>> GetAllCategory(CategoryInput input);
        Task<CategoryDto> GetCategory(int id);
        
        Task CreateCategory(CategoryInput input);

        Task Updatecategory(CategoryDto id);

        Task DeleteCategory(int id);
    }

}
