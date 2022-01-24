using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItpsController : ControllerBase
    {
        private IItpService _itpService;
        public ItpsController(IItpService itpService)
        {
            _itpService = itpService;
        }

        // GET: api/<ItpsController>
        [HttpGet]
        public IActionResult GetItps()
        {
            try
            {
                var all = _itpService.GetItps();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ItpsController>/5
        [HttpGet]
        public IActionResult GetItpById(int itpId)
        {
            try
            {
                var all = _itpService.GetItpById(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ItpsController>
        [HttpPost]
        public IActionResult PostItp(ItpDto itpDto)
        {
            try
            {
                var all = _itpService.AddItp(itpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ItpsController>/5
        [HttpPut]
        public IActionResult PutItp(ItpDto itpDto)
        {
            try
            {
                var all = _itpService.EditItp(itpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<ItpsController>/5
        [HttpDelete]
        public IActionResult DeleteItp(List<ItpDto> itpDto)
        {
            try
            {
                var all = _itpService.DeleteItp(itpDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult ItpStatus(int itpId, int status)
        {
            try
            {
                var all = _itpService.ItpStatus(itpId, status);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult ItpIsPublished(int itpId, bool isPublished)
        {
            try
            {
                var all = _itpService.ItpIsPublished(itpId, isPublished);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
