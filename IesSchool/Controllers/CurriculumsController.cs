using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurriculumsController : ControllerBase
    {
        private ICurriculumService _curriculumService;
        public CurriculumsController(ICurriculumService curriculumService)
        {
            _curriculumService = curriculumService;
        }

        // GET: api/<CurriculumsController>
        [HttpGet]
        public IActionResult GetAreas()
        {
            try
            {
                var all = _curriculumService.GetAreas();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<CurriculumsController>/5
        [HttpGet]
        public IActionResult GetAreaById(int areaId)
        {
            try
            {
                var all = _curriculumService.GetAreaById(areaId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<CurriculumsController>
        [HttpPost]
        public IActionResult PostArea(AreaDto areaDto)
        {
            try
            {
                var all = _curriculumService.AddArea(areaDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<CurriculumsController>/5
        [HttpPut]
        public IActionResult PutArea(AreaDto areaDto)
        {
            try
            {
                var all = _curriculumService.EditArea(areaDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<CurriculumsController>/5
        [HttpDelete]
        public IActionResult DeleteArea(int areaId)
        {
            try
            {
                var all = _curriculumService.DeleteArea(areaId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // GET: api/<CurriculumsController>
        [HttpGet]
        public IActionResult GetStrands()
        {
            try
            {
                var all = _curriculumService.GetStrands();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStrandsGroupByArea()
        {
            try
            {
                var all = _curriculumService.GetStrandsGroupByArea();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<CurriculumsController>/5
        [HttpGet]
        public IActionResult GetStrandByID(int strandId)
        {
            try
            {
                var all = _curriculumService.GetStrandById(strandId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<CurriculumsController>
        [HttpPost]
        public IActionResult PostStrand(StrandDto strandDto)
        {
            try
            {
                var all = _curriculumService.AddStrand(strandDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<CurriculumsController>/5
        [HttpPut]
        public IActionResult PutStrand(StrandDto strandDto)
        {
            try
            {
                var all = _curriculumService.EditStrand(strandDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<CurriculumsController>/5
        [HttpDelete]
        public IActionResult DeleteStrand(int strandId)
        {
            try
            {
                var all = _curriculumService.DeleteStrand(strandId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/<CurriculumsController>
        [HttpGet]
        public IActionResult GetSkills()
        {
            try
            {
                var all = _curriculumService.GetSkills();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetSkillsGroupByStrand()
        {
            try
            {
                var all = _curriculumService.GetSkillsGroupByStrand();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<CurriculumsController>/5
        [HttpGet]
        public IActionResult GetSkillByID(int skillId)
        {
            try
            {
                var all = _curriculumService.GetSkillById(skillId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<CurriculumsController>
        [HttpPost]
        public IActionResult PostSkill(SkillDto skillDto)
        {
            try
            {
                var all = _curriculumService.AddSkill(skillDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<CurriculumsController>/5
        [HttpPut]
        public IActionResult PutSkill(SkillDto skillDto)
        {
            try
            {
                var all = _curriculumService.EditSkill(skillDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<CurriculumsController>/5
        [HttpDelete]
        public IActionResult DeleteSkill(int skillId)
        {
            try
            {
                var all = _curriculumService.DeleteSkill(skillId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAreaByIdWithStrands(int areaId)
        {
            try
            {
                var all = _curriculumService.GetAreaByIdWithStrands(areaId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
