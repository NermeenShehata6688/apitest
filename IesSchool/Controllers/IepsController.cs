using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IepsController : ControllerBase
    {
        private IIepService _iepService;
        public IepsController(IIepService iepService)
        {
            _iepService = iepService;
        }
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetIepsHelper()
        {
            try
            {
                var all = _iepService.GetIepsHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<IepsController>
        [HttpGet]
        public IActionResult GetIeps([FromQuery] IepSearchDto iepSearchDto)
        {
            try
            {
                var all = _iepService.GetIeps(iepSearchDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET api/<IepsController>/5
        [HttpGet]
        public IActionResult GetIepById(int iepId)
        {
            try
            {
                var all = _iepService.GetIepById(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // POST api/<IepsController>
        [HttpPost]
        public IActionResult PostIep(IepDto iepDto)
        {
            try
            {
                var all = _iepService.AddIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // PUT api/<IepsController>/5
        [HttpPut]
        public IActionResult PutIep(IepDto iepDto)
        {
            try
            {
                var all = _iepService.EditIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // DELETE api/<IepsController>/5
        [HttpDelete]
        public IActionResult DeleteIep(int iepId)
        {
            try
            {
                var all = _iepService.DeleteIep(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IepStatus(int iepId, int status)
        {
            try
            {
                var all = _iepService.IepStatus(iepId, status);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IepIsPublished(int iepId, bool isPublished)
        {
            try
            {
                var all = _iepService.IepIsPublished(iepId, isPublished);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IepObjectiveMasterdPercentage(int iepId)
        {
            try
            {
                var all = _iepService.IepObjectiveMasterdPercentage(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/<IepsController>
        [HttpGet]
        public IActionResult GetGoals()
        {
            try
            {
                var all = _iepService.GetGoals();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<IepsController>/5
        [HttpGet]
        public IActionResult GetGoalById(int goalId)
        {
            try
            {
                var all = _iepService.GetGoalById(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<IepsController>
        [HttpPost]
        public IActionResult PostGoal(GoalDto goalDto)
        {
            try
            {
                var all = _iepService.AddGoal(goalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<IepsController>/5
        [HttpPut]
        public IActionResult PutGoal(GoalDto goalDto)
        {
            try
            {
                var all = _iepService.EditGoal(goalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<IepsController>/5
        [HttpDelete]
        public IActionResult DeleteGoal(int goalId)
        {
            try
            {
                var all = _iepService.DeleteGoal(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetObjectives()
        {
            try
            {
                var all = _iepService.GetObjectives();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetObjectiveById(int objectiveId)
        {
            try
            {
                var all = _iepService.GetObjectiveById(objectiveId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostObjective(ObjectiveDto objectiveDto)
        {
            try
            {
                var all = _iepService.AddObjective(objectiveDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutObjective(ObjectiveDto objectiveDto)
        {
            try
            {
                var all = _iepService.EditObjective(objectiveDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteObjective(int objectiveId)
        {
            try
            {
                var all = _iepService.DeleteObjective(objectiveId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPut]
        //public IActionResult ObjectiveIsMasterd(int objectiveId, bool isMasterd)
        //{
        //    try
        //    {
        //        var all = _iepService.ObjectiveIsMasterd(objectiveId, isMasterd);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpGet]
        public IActionResult GetActivities()
        {
            try
            {
                var all = _iepService.GetActivities();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetActivityById(int activityId)
        {
            try
            {
                var all = _iepService.GetActivityById(activityId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostActivity(ActivityDto activityDto)
        {
            try
            {
                var all = _iepService.AddActivity(activityDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutActivity(ActivityDto activityDto)
        {
            try
            {
                var all = _iepService.EditActivity(activityDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteActivity(int activityId)
        {
            try
            {
                var all = _iepService.DeleteActivity(activityId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpGet]
        public IActionResult GetIepParamedicalServiceById(int iepParamedicalServiceId)
        {
            try
            {
                var all = _iepService.GetIepParamedicalServiceById(iepParamedicalServiceId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var all = _iepService.AddIepParamedicalService(iepParamedicalServiceDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var all = _iepService.EditIepParamedicalService(iepParamedicalServiceDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteIepParamedicalService(int iepParamedicalServiceId)
        {
            try
            {
                var all = _iepService.DeleteIepParamedicalService(iepParamedicalServiceId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetIepExtraCurricularById(int iepExtraCurricularId)
        {
            try
            {
                var all = _iepService.GetIepExtraCurricularById(iepExtraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                var all = _iepService.AddIepExtraCurricular(iepExtraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                var all = _iepService.EditIepExtraCurricular(iepExtraCurricularDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteIepExtraCurricular(int iepExtraCurricularId)
        {
            try
            {
                var all = _iepService.DeleteIepExtraCurricular(iepExtraCurricularId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
