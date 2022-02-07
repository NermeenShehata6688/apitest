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
                return JsonSerializer.Serialize(all);
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
                return JsonSerializer.Serialize(all);
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
                return JsonSerializer.Serialize(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public FileStreamResult ItpReport(int itpId)
        {
            try
            {
                var all = _reportService.ItpReport(itpId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult ItpProgressReport(int itpProgressReportId)
        {
            try
            {
                var all = _reportService.ItpProgressReport(itpProgressReportId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string ItpReportHTML(int itpId)
        {
            try
            {
                var all = _reportService.ItpReportHTML(itpId);
                return JsonSerializer.Serialize(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string ItpProgressReportHTML(int itpProgressReportId)
        {
            try
            {
                var all = _reportService.ItpProgressReportHTML(itpProgressReportId);
                return JsonSerializer.Serialize(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult IxpReport(int ixpId)
        {
            try
            {
                var all = _reportService.IxpReport(ixpId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string IxpReportHTML(int ixpId)
        {
            try
            {
                var all = _reportService.IxpReportHTML(ixpId);
                return JsonSerializer.Serialize(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
