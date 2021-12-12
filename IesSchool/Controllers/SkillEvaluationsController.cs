using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillEvaluationsController : ControllerBase
    {
        private ISkillEvaluationService _skillEvaluationService;
        public SkillEvaluationsController(ISkillEvaluationService skillEvaluationService)
        {
            _skillEvaluationService = skillEvaluationService;
        }

        // GET: api/<SkillEvaluationsController>
        [HttpGet]
        public IActionResult GetSkillEvaluations()
        {
            try
            {
                var all = _skillEvaluationService.GetSkillEvaluations();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<SkillEvaluationsController>/5
        [HttpGet]
        public IActionResult GetSkillEvaluationById(int skillEvaluationId)
        {
            try
            {
                var all = _skillEvaluationService.GetSkillEvaluationById(skillEvaluationId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<SkillEvaluationsController>
        [HttpPost]
        public IActionResult PostSkillEvaluation(SkillEvaluationDto skillEvaluationDto)
        {
            try
            {
                var all = _skillEvaluationService.AddSkillEvaluation(skillEvaluationDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<SkillEvaluationsController>/5
        [HttpPut]
        public IActionResult PutSkillEvaluation(SkillEvaluationDto skillEvaluationDto)
        {
            try
            {
                var all = _skillEvaluationService.EditSkillEvaluation(skillEvaluationDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<SkillEvaluationsController>/5
        [HttpDelete]
        public IActionResult DeleteSkillEvaluation(int skillEvaluationId)
        {
            try
            {
                var all = _skillEvaluationService.DeleteSkillEvaluation(skillEvaluationId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
