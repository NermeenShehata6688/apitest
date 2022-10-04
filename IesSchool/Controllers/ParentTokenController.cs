using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParentTokenController : ControllerBase
    {
        private IParentTokenService _parentTokenService;
        public ParentTokenController(IParentTokenService parentTokenService)
        {
            _parentTokenService = parentTokenService;
        }
        [HttpGet]
        public IActionResult GetParentToken()
        {
            try
            {
                var all = _parentTokenService.GetParentToken();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetParentTokenByParentId(int parentId)
        {
            try
            {
                var all = _parentTokenService.GetParentTokenByParentId(parentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
