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

        //[ResponseCache(Duration = 3600)]
        [HttpGet]
        public async Task<IActionResult> GetItpsHelper()
        {
            try
            {
                //var all = _itpService.GetItpsHelper(); 
                var all =await _itpService.GetItpsHelperDapper(); 
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<ItpsController>
        [HttpGet]
        public IActionResult GetItps([FromQuery] ItpSearchDto itpSearchDto)
        {
            try
            {
                var all = _itpService.GetItps(itpSearchDto);
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
        public IActionResult DeleteItp(int itpId)
        {
            try
            {
                var all = _itpService.DeleteItp(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult ItpStatus(StatusDto statusDto)
        {
            try
            {
                var all = _itpService.ItpStatus(statusDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult ItpIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                var all = _itpService.ItpIsPublished(isPuplishedDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult ItpDuplicate(int itpId)
        {
            try
            {
                var all = _itpService.ItpDuplicate(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetItpObjectiveByGoalId(int itpGoalId)
        {
            try
            {
                var all = _itpService.GetItpObjectiveByGoalId(itpGoalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetGoalByItpId(int itpId)
        {
            try
            {
                var all = _itpService.GetGoalByItpId(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostItpGoal(ItpGoalDto itpGoalDto)
        {
            try
            {
                var all = _itpService.AddItpGoal(itpGoalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutItpGoal(ItpGoalDto goalDto)
        {
            try
            {
                var all = _itpService.EditItpGoal(goalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteItpGoal(int goalId)
        {
            try
            {
                var all = _itpService.DeleteItpGoal(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetItpProgressReportsByItpId(int itpId)
        {
            try
            {
                var all = _itpService.GetItpProgressReportsByItpId(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetItpProgressReportById(int itpProgressReportId)
        {
            try
            {
                var all = _itpService.GetItpProgressReportById(itpProgressReportId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                var all = _itpService.AddItpProgressReport(itpProgressReportDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                var all = _itpService.EditItpProgressReport(itpProgressReportDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteItpProgressReport(int itpProgressReportId)
        {
            try
            {
                var all = _itpService.DeleteItpProgressReport(itpProgressReportId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult CreateItpProgressReport(int itpId)
        {
            try
            {
                var all = _itpService.CreateItpProgressReport(itpId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult  GetIepsForTherapist(int therapistId)
        {
            try
            {
                var all = _itpService.GetIepsForTherapist(therapistId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult CreateItp(int iepParamedicalServiceId)
        {
            try
            {
                var all = _itpService.CreateItp(iepParamedicalServiceId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetObjectiveByITPId(int itpId)
        {
            try
            {
                var all = _itpService.GetObjectiveByITPId(itpId);
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
                var all = _itpService.DeleteObjective(objectiveId);
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
                var all = _itpService.GetObjectiveById(objectiveId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult EditObjectiveActivities(ItpGoalObjectiveActivitiesDto objectiveActivitiesDto)
        {
            try
            {
                var all = _itpService.EditObjectiveActivities(objectiveActivitiesDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetActivityByObjectiveId(int objectiveId)
        {
            try
            {
                var all = _itpService.GetActivityByObjectiveId(objectiveId);
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
                var all = _itpService.DeleteActivity(activityId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
