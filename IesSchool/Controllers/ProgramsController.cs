using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private IProgramsService _programsService;
        public ProgramsController(IProgramsService programsService)
        {
            _programsService = programsService;
        }

        /// <summary>
        /// This is method summary I want displayed
        /// </summary>
        [HttpGet]
        public IActionResult GetPrograms()
        {
            try
            {
                var all = _programsService.GetPrograms();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ProgramsController>/5
        [HttpGet]
        public IActionResult GetProgramById(int programId)
        {
            try
            {
                var all = _programsService.GetProgramById(programId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ProgramsController>
        [HttpPost]
        public IActionResult PostProgram(ProgramDto programDto)
        {
            try
            {
                var all = _programsService.AddProgram(programDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ProgramsController>/5
        [HttpPut]
        public IActionResult PutProgram(ProgramDto programDto)
        {
            try
            {
                var all = _programsService.EditProgram(programDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<ProgramsController>/5
        [HttpDelete]
        public IActionResult DeleteProgram(int programId)
        {
            try
            {
                var all = _programsService.DeleteProgram(programId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
