using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExtraCurricularsController : ControllerBase
    {
        private IExtraCurricularService _extraCurricularService;
        public ExtraCurricularsController(IExtraCurricularService extraCurricularService)
        {
            _extraCurricularService = extraCurricularService;
        }


        // GET: api/<ExtraCurricularsController>
        [HttpGet]
        public IActionResult GetExtraCurriculars()
        {
            try
            {
                var all = _extraCurricularService.GetExtraCurriculars();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ExtraCurricularsController>/5
        [HttpGet]
        public IActionResult GetExtraCurricularById(int extraCurricularId)
        {
            try
            {
                var all = _extraCurricularService.GetExtraCurricularById(extraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ExtraCurricularsController>
        [HttpPost]
        public IActionResult PostExtraCurricular(ExtraCurricularDto extraCurricularDto)
        {
            try
            {
                var all = _extraCurricularService.AddExtraCurricular(extraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ExtraCurricularsController>/5
        [HttpPut]
        public IActionResult PutExtraCurricular(ExtraCurricularDto extraCurricularDto)
        {
            try
            {
                var all = _extraCurricularService.EditExtraCurricular(extraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<ExtraCurricularsController>/5
        [HttpDelete]
        public IActionResult DeleteExtraCurricular(int extraCurricularId)
        {
            try
            {
                var all = _extraCurricularService.DeleteExtraCurricular(extraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
