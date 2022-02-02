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
        public IActionResult IsUserCodeExist(string UserName, string Password)
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
    }
}
