using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IIepService
    {
        #region Dapper
        public ResponseDto GetIepHelperDapper();
        public ResponseDto GetIepsHelper2Dapper();
        #endregion
        public ResponseDto GetIepsHelper();
        public ResponseDto GetIepsHelper2();
        public ResponseDto GetIeps(IepSearchDto iepSearchDto);
        public ResponseDto GetIepById(int iepId);
        public Task<ResponseDto> GetIepByIdDapper(int iepId);
        public Task<ResponseDto> AddIep(IepDto iepDto);
        public ResponseDto EditIep(IepDto iepDto);
        public Task<ResponseDto> EditIepLight(IepDto iepDto);

        public Task<ResponseDto> DeleteIep(int iepId);
        public ResponseDto IepStatus(StatusDto statusDto);
        public ResponseDto IepIsPublished(IsPuplishedDto isPuplishedDto);
        public decimal IepObjectiveMasterdPercentage(int iepId);
        public ResponseDto DuplicateIEP(int iepId);

        public ResponseDto GetGoals();
        public Task<ResponseDto> GetGoalById(int goalId);
        public Task<ResponseDto> GetGoalByIepId(int iepId);
        public ResponseDto AddGoal(GoalDto goalDto);
        public Task<ResponseDto> EditGoal(GoalDto goalDto);
        public ResponseDto DeleteGoal(int goalId);

        public ResponseDto GetObjectives();
        public Task<ResponseDto> GetObjectiveById(int objectiveId);
        public Task<ResponseDto> GetObjectiveByIEPId(int iepId);
        public ResponseDto AddObjective(ObjectiveDto objectiveDto);
        public Task<ResponseDto> EditObjectiveActivities(ObjectiveActivitiesDto objectiveDto);
        public ResponseDto DeleteObjective(int objectiveId);

        //public ResponseDto ObjIsMasterd(int objId, bool IsMasterd);
        public ResponseDto ObjIsMasterd(ObjStatus statusDto);

        public bool ObjectiveIsMasterd(Objective objective);

        public ResponseDto GetActivities();
        public Task<ResponseDto> GetActivityByObjectiveId(int objectiveId);
        public ResponseDto AddActivity(ActivityDto activityDto);
        public ResponseDto EditActivity(ActivityDto activityDto);
        public ResponseDto DeleteActivity(int activityId);

        public ResponseDto GetIepParamedicalServices();
        public Task<ResponseDto> GetIepParamedicalServiceByIepId(int iepId);
        public ResponseDto AddIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public Task<ResponseDto> EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto DeleteIepParamedicalService(int iepParamedicalServiceId);

        public ResponseDto GetIepExtraCurriculars();
        public Task<ResponseDto> GetIepExtraCurricularByIepId(int iepId);
        public ResponseDto AddIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public Task<ResponseDto> EditIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public ResponseDto DeleteIepExtraCurricular(int iepExtraCurricularId);


        public Task<ResponseDto> GetIepProgressReportsByIepId(int iepId);
        public Task<ResponseDto> GetIepProgressReportById(int iepProgressReportId);
        public ResponseDto AddIepProgressReport(IepProgressReportDto iepProgressReportDto);
        public Task<ResponseDto> EditIepProgressReport(IepProgressReportDto iepProgressReportDto);
        public Task<ResponseDto> DeleteIepProgressReport(int iepProgressReportId);
        public Task<ResponseDto> CreateIepProgressReport(int iepId);
        //public ResponseDto SyncData(int iepId);
        public ResponseDto GetProgressReportParamedicalByUserId(int userId);
        public ResponseDto GetProgressReportParamedicalById(int progressReportParamedicalId);
        public ResponseDto EditProgressReportParamedical(ProgressReportParamedicalDto progressReportParamedicalDto);
        public ResponseDto DeleteProgressReportParamedicalt(int progressReportParamedicalId);
        public ResponseDto GetSkillsByObjectiveId(int objectiveId);

    }
}
