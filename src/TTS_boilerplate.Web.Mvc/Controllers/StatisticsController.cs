using Microsoft.AspNetCore.Mvc;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.Statistics;
using TTS_boilerplate.Web.Models.Statistics;

namespace TTS_boilerplate.Web.Controllers
{
    public class StatisticsController : TTS_boilerplateControllerBase
    {
        private readonly IStatisticsAppService _statisticsAppService;
        public StatisticsController(IStatisticsAppService statisticsAppService)
        {
            _statisticsAppService = statisticsAppService;
        }
        public IActionResult Index_Statistic()
        {
            var topRevenueProducts = _statisticsAppService.TopRevenueProduct().Result;

            var model = new StatisticsViewModel
            {
                revenueProducts = topRevenueProducts
            };
            return View(model);
        }
    }
}
