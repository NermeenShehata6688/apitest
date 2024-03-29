﻿using IesSchool.Context.Models;
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
        private ICurriculumService _curriculumService;
        public IepsController(IIepService iepService, ICurriculumService curriculumService)
        {
            _iepService = iepService;
            _curriculumService = curriculumService;
        }
        // [admin(Duration = 800)]

        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public IActionResult GetIepsHelper()
        {
            try
            {
                //var all = _iepService.GetIepsHelper(); 
                var all = _iepService.GetIepHelperDapper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [ResponseCache(Duration = 3600)]
        [HttpGet]
        public IActionResult GetIepsHelper2()
        {
            try
            {
                var all = _iepService.GetIepsHelper2Dapper();
                //var all = _iepService.GetIepsHelper2();
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
        //[ResponseCache(Duration = 1000)]
        [HttpGet]
        public async Task<IActionResult> GetIepById(int iepId)
        {
            try
            {
                //var all = _iepService.GetIepById(iepId);
                var all = await _iepService.GetIepByIdDapper(iepId);

                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // POST api/<IepsController>
        [HttpPost]
        public async Task<IActionResult> PostIep(IepDto iepDto)
        {
            try
            {
                var all = await _iepService.AddIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // PUT api/<IepsController>/5
        [HttpPut]
        public async Task<IActionResult> PutIep(IepDto iepDto)
        {
            try
            {
                var all =await _iepService.EditIepLight(iepDto);
                //var all = _iepService.EditIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // DELETE api/<IepsController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteIep(int iepId)
        {
            try
            {
                var all =await _iepService.DeleteIep(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IepStatus(StatusDto statusDto)
        {
            try
            {
                var all = _iepService.IepStatus(statusDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IepIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                var all = _iepService.IepIsPublished(isPuplishedDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
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
        [HttpGet]
        public IActionResult DuplicateIEP(int iepId)
        {
            try
            {
                var all = _iepService.DuplicateIEP(iepId);
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
        public async Task<IActionResult> GetGoalById(int goalId)
        {
            try
            {
                var all =await _iepService.GetGoalById(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGoalsByIepId(int iepId)
        {
            try
            {
                var all =await    _iepService.GetGoalByIepId(iepId);
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
        public async Task<IActionResult> PutGoal(GoalDto goalDto)
        {
            try
            {
                var all = await _iepService.EditGoal(goalDto);
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
        public async Task<IActionResult> GetObjectiveById(int objectiveId)
        {
            try
            {
                var all = await _iepService.GetObjectiveById(objectiveId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetObjectiveByIEPId(int iepId)
        {
            try
            {
                var all = await _iepService.GetObjectiveByIEPId(iepId);
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
        public async Task<IActionResult> EditObjectiveActivities(ObjectiveActivitiesDto objectiveActivitiesDto)
        {
            try
            {
                var all = await _iepService.EditObjectiveActivities(objectiveActivitiesDto);
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
        [HttpPut]
        public IActionResult ObjIsMasterd(ObjStatus status)
        {
            try
            {
                var all = _iepService.ObjIsMasterd(status);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPut]
        //public IActionResult ObjIsMasterd(int objId, bool isMastered)
        //{
        //    try
        //    {
        //        var all = _iepService.ObjIsMasterd(objId, isMastered);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
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
        public async Task<IActionResult> GetActivityByObjectiveId(int objectiveId)
        {
            try
            {
                var all = await _iepService.GetActivityByObjectiveId(objectiveId);
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
        public async Task<IActionResult> GetIepParamedicalServiceByIepId(int iepId)
        {
            try
            {
                var all = await _iepService.GetIepParamedicalServiceByIepId(iepId);
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
        public async Task<IActionResult> PutIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var all = await _iepService.EditIepParamedicalService(iepParamedicalServiceDto);
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
        public async Task<IActionResult> GetIepExtraCurricularByIepId(int iepId)
        {
            try
            {
                var all = await _iepService.GetIepExtraCurricularByIepId(iepId);
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
        public async Task<IActionResult> PutIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                var all = await _iepService.EditIepExtraCurricular(iepExtraCurricularDto);
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

        [HttpGet]
        public async Task<IActionResult> GetIepProgressReportsByIepId(int iepId)
        {
            try
            {
                var all = await _iepService.GetIepProgressReportsByIepId(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetIepProgressReportById(int iepId)
        {
            try
            {
                var all = await _iepService.GetIepProgressReportById(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostIepProgressReport(IepProgressReportDto iepProgressReportDto)
        {
            try
            {
                var all = _iepService.AddIepProgressReport(iepProgressReportDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutIepProgressReport(IepProgressReportDto iepProgressReportDto)
        {
            try
            {
                var all = await _iepService.EditIepProgressReport(iepProgressReportDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteIepProgressReport(int iepProgressReportId)
        {
            try
            {
                var all = await _iepService.DeleteIepProgressReport(iepProgressReportId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateIepProgressReport(int iepId)
        {
            try
            {
                var all = await _iepService.CreateIepProgressReport(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetProgressReportParamedicalByUserId(int userId)
        {
            try
            {
                var all = _iepService.GetProgressReportParamedicalByUserId(userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetProgressReportParamedicalById(int progressReportParamedicalId)
        {
            try
            {
                var all = _iepService.GetProgressReportParamedicalById(progressReportParamedicalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutIProgressReportParamedical(ProgressReportParamedicalDto progressReportParamedicalDto)
        {
            try
            {
                var all = _iepService.EditProgressReportParamedical(progressReportParamedicalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteProgressReportParamedicalt(int progressReportParamedicalId)
        {
            try
            {
                var all = _iepService.DeleteProgressReportParamedicalt(progressReportParamedicalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetSkillsByStrandId(int strandId)
        {
            try
            {
                var all = _curriculumService.GetSkillsByStrandId(strandId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetSkillsByObjectiveId(int objectiveId)
        {
            try
            {
                var all = _iepService.GetSkillsByObjectiveId(objectiveId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
