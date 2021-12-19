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
        public ResponseDto DeleteIep(int iepId);
        public ResponseDto IepStatus(int iepId, int status); 
        public ResponseDto IepIsPublished(int iepId, bool isPublished);
        public decimal IepObjectiveMasterdPercentage(int iepId);

        public ResponseDto GetGoals();
        public ResponseDto GetGoalById(int goalId);
        public ResponseDto AddGoal(GoalDto goalDto);
        public ResponseDto EditGoal(GoalDto goalDto);
        public ResponseDto DeleteGoal(int goalId);

        public ResponseDto GetObjectives();
        public ResponseDto GetObjectiveById(int objectiveId);
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
        public ResponseDto GetIepParamedicalServiceById(int iepParamedicalServiceId);
        public ResponseDto AddIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto DeleteIepParamedicalService(int iepParamedicalServiceId);

        public ResponseDto GetIepExtraCurriculars(); 
        public ResponseDto GetIepExtraCurricularById(int iepExtraCurricularId);
        public ResponseDto AddIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public ResponseDto EditIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto);
        public ResponseDto DeleteIepExtraCurricular(int iepExtraCurricularId);
    }
}
