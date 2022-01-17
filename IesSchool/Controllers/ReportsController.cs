using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.XlsIO;
using System.Text.Json;

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
        //[ResponseCache(Duration = 800)]
        //[HttpGet]
        //public IActionResult GetReporstHelper()
        //{
        //    try
        //    {
        //        var all = _reportService.GetReporstHelper();
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpGet]
        public FileStreamResult LpReport(int iepId)
        {
            try
            {
                var all = _reportService.IepLpReport(iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult IepReport(int iepId)
        {
            try
            {
                var all = _reportService.IepReport(iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult BCPReport(int? studentId, int? iepId)
        {
            try
            {
                var all = _reportService.BCPReport(studentId, iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult ProgressReport(int iepProgressReportId)
        {
            try
            {
                var all = _reportService.ProgressReport(iepProgressReportId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public string IepReportHTML(int iepId)
        {
            try
            {
                var all = _reportService.IepReportHTML(iepId);
              
                return JsonSerializer.Serialize(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public string ProgressReportHTML(int iepProgressReportId)
        {
            try
            {
                var all = _reportService.ProgressReportHTML(iepProgressReportId);
                return (all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public string BCPReportHTML(int? studentId, int? iepId)
        {
            try
            {
                var all = _reportService.BCPReportHTML(studentId, iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public string LpReportHTML(int iepId)
        {
            try
            {
                var all = _reportService.IepLpReportHTML(iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
