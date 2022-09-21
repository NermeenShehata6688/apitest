using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
//    //[Authorize]
  //  [Route("secure/[controller]/[action]")]
  [Route("api/[controller]/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private IMobileService _iMobileService;
        private IReportService _reportService;
        private IEmailSenderService _emailSenderService;
        private ISettingService _settingService;

        public MobileController(IMobileService iMobileService, IReportService reportService, 
            IEmailSenderService emailSenderService, ISettingService settingService)
        {
            _iMobileService = iMobileService;
            _reportService = reportService;
            _emailSenderService = emailSenderService;
            _settingService = settingService;
        }

        //[HttpGet]
        //public IActionResult Login(string UserName, string Password)
        //{
        //    try
        //    {
        //        var all = _iMobileService.Login(UserName, Password);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        public IActionResult Login(string CivilId, string Password)
        {
            try
            {
                var all = _iMobileService.Login(CivilId, Password);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public IActionResult GetEvents([FromQuery] GetMobileEventsDto getMobileEventsDto)
        {
            try
            {
                    var all = _iMobileService.GetEvents(getMobileEventsDto);
                    return Ok(all);
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetParentStudents(int parentId)
        {
            try
            {
                var all = _iMobileService.GetParentStudents(parentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetEventById(int eventId, int? parentId)
        {
            try
            {
                var all = _iMobileService.GetEventById(eventId, parentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet]
        //public IActionResult GetStudentsEventsByParentId(int parentId)
        //{
        //    try
        //    {
        //        var all = _iMobileService.GetStudentsEventsByParentId(parentId);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpGet]
        public IActionResult GetStudentIeps(int studentId)
        {
            try
            {
                var all = _iMobileService.GetStudentIeps(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStudentItps(int studentId)
        {
            try
            {
                var all = _iMobileService.GetStudentItps(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStudentIxps(int studentId)
        {
            try
            {
                var all = _iMobileService.GetStudentIxps(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult SendEmail(string email)
        {
            try
            {
                var all = _emailSenderService.SendEmail(email);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            try
            {
                var all = _iMobileService.ChangePassword(id, oldPassword, newPassword);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        [HttpGet]
        public FileStreamResult LpReportPDF(int iepId)
        {
            try
            {
                var all = _iMobileService.IepLpReportPDF(iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult LpReportPdfPreview(int iepId)
        {
            try
            {
                var all = _iMobileService.IepLpReportPdfPreview(iepId);
                return Ok( all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult IepReportPDF(int iepId)
        {
            try
            {
                var all = _iMobileService.IepReportPDF(iepId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult IepReportPdfPreview(int iepId)
        {
            try
            {
                var all = _iMobileService.IepReportPdfPreview(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult IepProgressReportPDF(int IepProgressReportPDF)
        {
            try
            {
                var all = _iMobileService.IepProgressReportPDF(IepProgressReportPDF);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult IepProgressReportPdfPreview(int iepId)
        {
            try
            {
                var all = _iMobileService.IepProgressReportPdfPreview(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult ItpReportPDF(int itpId)
        {
            try
            {
                var all = _iMobileService.ItpReportPDF(itpId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult ItpReportPdfPreview(int itpId)
        {
            try
            {
                var all = _iMobileService.ItpReportPdfPreview(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult ItpProgressReportPDF(int itpProgressReportId)
        {
            try
            {
                var all = _iMobileService.ItpProgressReportPDF(itpProgressReportId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult ItpProgressReportPdfPreview(int itpProgressReportId)
        {
            try
            {
                var all = _iMobileService.ItpProgressReportPdfPreview(itpProgressReportId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public FileStreamResult IxpReportPDF(int ixpId)
        {
            try
            {
                var all = _iMobileService.IxpReportPDF(ixpId);
                return all;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult IxpReportPdfPreview(int ixpId)
        {
            try
            {
                var all = _iMobileService.IxpReportPdfPreview(ixpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult AboutUs()
        {
            try
            {
                var all = _settingService.AboutUs();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult GetStudentProgressReports(int studentId)
        {
            try
            {
                var all = _iMobileService.GetStudentProgressReports(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
