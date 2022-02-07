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
        public MobileController(IMobileService iMobileService)
        {
            _iMobileService = iMobileService;
        }


        // GET: api/<MobileController>
        [HttpGet]
        public IActionResult IsParentExist(string UserName, string Password)
        {
            try
            {
                var all = _iMobileService.IsParentExist(UserName, Password);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult ReturnParentIfExist(string UserName, string Password)
        {
            try
            {
                var all = _iMobileService.ReturnParentIfExist(UserName, Password);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetParentById(int parentId)
        {
            try
            {
                var all = _iMobileService.GetParentById(parentId);
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
        public IActionResult GetParentStudentsEvents(int parentId)
        {
            try
            {
                var all = _iMobileService.GetParentStudentsEvents(parentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
