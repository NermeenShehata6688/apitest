using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private IStatisticService _statisticService;
        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet]
        public IActionResult GetStatistics()
        {
            try
            {
                var all = _statisticService.GetStatistics();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
