using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET api/<ReportsController>/5
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetReporstHelper()
        {
            try
            {
                var all = _reportService.GetReporstHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult IepLpReport(int iepId)
        {
            try
            {
                var all = _reportService.IepLpReport(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
