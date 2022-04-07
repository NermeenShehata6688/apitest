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

      // [ResponseCache(Duration = 800)]
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
        public IActionResult DeleteIxp(int ixpId)
        {
            try
            {
                var all = _ixpService.DeleteIxp(ixpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IxpStatus(StatusDto statusDto)
        {
            try
            {
                var all = _ixpService.IxpStatus(statusDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IxpIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                var all = _ixpService.IxpIsPublished(isPuplishedDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult IxpDuplicate(int ixpId)
        {
            try
            {
                var all = _ixpService.IxpDuplicate(ixpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetIxpExtraCurricularByIxpId(int ixpId)
        {
            try
            {
                var all = _ixpService.GetIxpExtraCurricularByIxpId(ixpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetIxpExtraCurricularById(int ixpExtraCurricularId)
        {
            try
            {
                var all = _ixpService.GetIxpExtraCurricularById(ixpExtraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto)
        {
            try
            {
                var all = _ixpService.AddIxpExtraCurricular(ixpExtraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto)
        {
            try
            {
                var all = _ixpService.EditIxpExtraCurricular(ixpExtraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteIxpExtraCurricular(int ixpExtraCurricularId)
        {
            try
            {
                var all = _ixpService.DeleteIxpExtraCurricular(ixpExtraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetIepsForExTeacher(int teacherId)
        {
            try
            {
                var all = _ixpService.GetIepsForExTeacher(teacherId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult CreateIxp(int iepExtraCurricularId)
        {
            try
            {
                var all = _ixpService.CreateIxp(iepExtraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
