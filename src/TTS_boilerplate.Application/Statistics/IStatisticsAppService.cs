using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTS_boilerplate.Statistics.Dto;

namespace TTS_boilerplate.Statistics
{
    public interface  IStatisticsAppService : IApplicationService
    {
        Task<ListResultDto<chartCategoryDto>> GetDataForChartCategory();

        Task<List<chartCategoryDto>> TotalAmountOfCategory();

        Task<List<RevenueProduct>> RevenueByProduct();
        Task<RevenueProduct> TotalRevenueProduct();
        Task<List<RevenueProduct>> TopRevenueProduct();
    }
}
