using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
   //[Authorize]
    [Route("secure/[controller]/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private IMobileService _iMobileService;
        private IReportService _reportService;
        private IEventService _eventService;
        public MobileController(IMobileService iMobileService, IReportService reportService, IEventService eventService)
        {
            _iMobileService = iMobileService;
            _reportService = reportService;
            _eventService = eventService;
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
        public IActionResult GetEventById(int eventId)
        {
            try
            {
                var all = _eventService.GetEventById(eventId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStudentsEventsByParentId(int parentId)
        {
            try
            {
                var all = _iMobileService.GetStudentsEventsByParentId(parentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStudentIepsItpsIxps(int studentId)
        {
            try
            {
                var all = _iMobileService.GetStudentIepsItpsIxps(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
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
        [HttpPut]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            try
            {
                var all = _iMobileService.ChangePassword(id,  oldPassword,  newPassword);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
