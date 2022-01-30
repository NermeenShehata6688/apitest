using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IxpsController : ControllerBase
    {
        private IIxpService _ixpService;
        public IxpsController(IIxpService ixpService)
        {
            _ixpService = ixpService;
        }

        //[ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetIxpsHelper()
        {
            try
            {
                var all = _ixpService.GetIxpsHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<IxpsController>
        [HttpGet]
        public IActionResult GetIxps([FromQuery] IxpSearchDto ixpSearchDto)
        {
            try
            {
                var all = _ixpService.GetIxps(ixpSearchDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET api/<IxpsController>/5
        [HttpGet]
        public IActionResult GetIxpById(int ixpId)
        {
            try
            {
                var all = _ixpService.GetIxpById(ixpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<IxpsController>
        [HttpPost]
        public IActionResult PostIxp(IxpDto ixpDto)
        {
            try
            {
                var all = _ixpService.AddIxp(ixpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<IxpsController>/5
        [HttpPut]
        public IActionResult PutIxp(IxpDto ixpDto)
        {
            try
            {
                var all = _ixpService.EditIxp(ixpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<IxpsController>/5
        [HttpDelete]
        public IActionResult DeleteIxp(List<IxpDto> ixpDto)
        {
            try
            {
                var all = _ixpService.DeleteIxp(ixpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IxpStatus(int ixpId, int status)
        {
            try
            {
                var all = _ixpService.IxpStatus(ixpId, status);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IxpIsPublished(int ixpId, bool isPublished)
        {
            try
            {
                var all = _ixpService.IxpIsPublished(ixpId, isPublished);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
