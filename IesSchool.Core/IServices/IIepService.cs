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
        public ResponseDto GetIepsHelper();
        public ResponseDto GetIeps(IepSearchDto iepSearchDto);
        public ResponseDto GetIepById(int iepId);
        public ResponseDto AddIep(IepDto iepDto);
        public ResponseDto EditIep(IepDto iepDto); 
        public ResponseDto DeleteIep(List<IepDto> iepDto);
        public ResponseDto IepStatus(int iepId, int status); 
        public ResponseDto IepIsPublished(int iepId, bool isPublished);
        public decimal IepObjectiveMasterdPercentage(int iepId);

        public ResponseDto GetGoals();
        public ResponseDto GetGoalById(int goalId);
        public ResponseDto GetGoalByIepId(int iepId);
        public ResponseDto AddGoal(GoalDto goalDto);
        public ResponseDto EditGoal(GoalDto goalDto);
        public ResponseDto DeleteGoal(int goalId);

        public ResponseDto GetObjectives(); 
        public ResponseDto GetObjectiveById(int objectiveId);
        public ResponseDto GetObjectiveByIEPId(int iepId);
        public ResponseDto AddObjective(ObjectiveDto objectiveDto);
        public ResponseDto EditObjective(ObjectiveDto objectiveDto);
        public ResponseDto DeleteObjective(int objectiveId);
        public bool ObjectiveIsMasterd(Objective objective);

        public ResponseDto GetActivities();
        public ResponseDto GetActivityById(int activityId);
        public ResponseDto AddActivity(ActivityDto activityDto);
        public ResponseDto EditActivity(ActivityDto activityDto);
        public ResponseDto DeleteActivity(int activityId);

        public ResponseDto GetIepParamedicalServices();
        public ResponseDto GetIepParamedicalServiceByIepId(int iepId);
        public ResponseDto AddIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto DeleteIepParamedicalService(int iepParamedicalServiceId);

        public ResponseDto GetIepExtraCurriculars(); 
        public ResponseDto GetIepExtraCurricularByIepId(int iepId);
        public ResponseDto AddIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public ResponseDto EditIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public ResponseDto DeleteIepExtraCurricular(int iepExtraCurricularId);


        public ResponseDto GetIepProgressReportsByIepId(int iepId);
        public ResponseDto GetIepProgressReportById(int iepProgressReportId);
        public ResponseDto AddIepProgressReport(IepProgressReportDto iepProgressReportDto);
        public ResponseDto EditIepProgressReport(IepProgressReportDto iepProgressReportDto);
        public ResponseDto DeleteIepProgressReport(int iepProgressReportId);
        public ResponseDto CreateIepProgressReport(int iepId);
        //public ResponseDto SyncData(int iepId);

    }
}
