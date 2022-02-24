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

        public MobileController(IMobileService iMobileService, IReportService reportService, IEmailSenderService emailSenderService)
        {
            _iMobileService = iMobileService;
            _reportService = reportService;
            _emailSenderService = emailSenderService;
        }
      
        [HttpGet]
        public IActionResult Login(string UserName, string Password)
        {
            try
            {
                var all = _iMobileService.Login(UserName, Password);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[ResponseCache(Duration = 3600)]
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
        [HttpPut]
        public ActionResult SendEmail(PasswordResetDto passwordResetDto)
        {
            try
            {
                var all = _emailSenderService.SendEmail(passwordResetDto);
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

    }
}
