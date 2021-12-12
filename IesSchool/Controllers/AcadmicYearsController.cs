using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AcadmicYearsController : ControllerBase
    {
        private IAcadmicYearsService _acadmicYearsService;
        public AcadmicYearsController(IAcadmicYearsService acadmicYearsService)
        {
            _acadmicYearsService = acadmicYearsService;
        }

        // GET: api/<AcadmicYearsController>
        [HttpGet]
        public IActionResult GetAcadmicYears()
        {
            try
            {
                var all = _acadmicYearsService.GetAcadmicYears();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<AcadmicYearsController>/5
        [HttpGet]
        public IActionResult GetAcadmicYearById(int acadmicYearId)
        {
            try
            {
                var all = _acadmicYearsService.GetAcadmicYearById(acadmicYearId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AcadmicYearsController>
        [HttpPost]
        public IActionResult PostAcadmicYear(AcadmicYearDto acadmicYearDto)
        {
            try
            {
                var all = _acadmicYearsService.AddAcadmicYear(acadmicYearDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<AcadmicYearsController>/5
        [HttpPut]
        public IActionResult PutAcadmicYear(AcadmicYearDto acadmicYearDto)
        {
            try
            {
                var all = _acadmicYearsService.EditAcadmicYear(acadmicYearDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<AcadmicYearsController>/5
        [HttpDelete]
        public IActionResult DeleteAcadmicYear(int acadmicYearId)
        {
            try
            {
                var all = _acadmicYearsService.DeleteAcadmicYear(acadmicYearId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
