using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssistantsController : ControllerBase
    {
        private IAssistantService _assistantService;
        public AssistantsController(IAssistantService assistantService)
        {
            _assistantService = assistantService;
        }
        // GET: api/<AssistantsController>
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetAssistantHelper()
        {
            try
            {
                var all = _assistantService.GetAssistantHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAssistants([FromQuery] AssistantSearchDto assistantSearchDto)
        {
            try
            {
                var all = _assistantService.GetAssistants(assistantSearchDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET api/<AssistantsController>/5
        [HttpGet]
        public IActionResult GetAssistantsById(int assistantsId)
        {
            try
            {
                var all = _assistantService.GetAssistantById(assistantsId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AssistantsController>
        [HttpPost]
        public IActionResult PostAssistant(AssistantDto assistantDto)
        {
            try
            {
                var all = _assistantService.AddAssistant(assistantDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<AssistantsController>/5
        [HttpPut]
        public IActionResult PutAssistant(AssistantDto assistantDto)
        {
            try
            {
                var all = _assistantService.EditAssistant(assistantDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<AssistantsController>/5
        [HttpDelete]
        public IActionResult DeleteAssistant(int assistantsId)
        {
            try
            {
                var all = _assistantService.DeleteAssistant(assistantsId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IsSuspended(int assistantId, bool isSuspended)
        {
            try
            {
                var all = _assistantService.IsSuspended(assistantId, isSuspended);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
