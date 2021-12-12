using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TermsController : ControllerBase
    {
        private ITermsService _termsServicee;
        public TermsController(ITermsService termsService)
        {
            _termsServicee = termsService;
        }

        // GET: api/<TermsController>
        [HttpGet]
        public IActionResult GetTerms()
        {
            try
            {
                var all = _termsServicee.GetTerms();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<TermsController>/5
        [HttpGet]
        public IActionResult GetTermById(int termId)
        {
            try
            {
                var all = _termsServicee.GetTermById(termId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<TermsController>
        [HttpPost]
        public IActionResult PostTerm(TermDto termDto)
        {
            try
            {
                var all = _termsServicee.AddTerm(termDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<TermsController>/5
        [HttpPut]
        public IActionResult PutTerm(TermDto termDto)
        {
            try
            {
                var all = _termsServicee.EditTerm(termDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<TermsController>/5
        [HttpDelete]
        public IActionResult DeleteTerm(int termId)
        {
            try
            {
                var all = _termsServicee.DeleteTerm(termId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
