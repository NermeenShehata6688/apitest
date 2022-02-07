using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private IMobileService _iMobileService;
        private IReportService _reportService;
        public MobileController(IMobileService iMobileService, IReportService reportService)
        {
            _iMobileService = iMobileService;
            _reportService = reportService;
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

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public IActionResult GetEventsImageGroubedByEventId()
        {
            try
            {
                var all = _iMobileService.GetEventsImageGroubedByEventId();
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
       

    }
}
