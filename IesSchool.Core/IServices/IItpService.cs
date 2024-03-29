﻿using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IItpService
    {
        #region Dapper
        public Task<ResponseDto> GetItpsHelperDapper();

        #endregion
        public ResponseDto GetItpsHelper();
        public ResponseDto GetItps(ItpSearchDto itpSearchDto);
        public ResponseDto GetItpById(int itpId);
        public ResponseDto AddItp(ItpDto itpDto);
        public ResponseDto EditItp(ItpDto itpDto);
        public ResponseDto DeleteItp(int itpId);
        public ResponseDto ItpStatus(StatusDto statusDto);
        public ResponseDto ItpIsPublished(IsPuplishedDto isPuplishedDto);
        public ResponseDto ItpDuplicate(int itpId);

        public ResponseDto GetItpGoals();
        public ResponseDto GetItpGoalById(int itpGoalId); 
        public ResponseDto GetGoalByItpId(int itpId);
        public ResponseDto AddItpGoal(ItpGoalDto itpGoalDto);
        public ResponseDto EditItpGoal(ItpGoalDto goalDto);
        public ResponseDto DeleteItpGoal(int goalId);

        public ResponseDto GetItpObjectiveByGoalId(int goalId);

        public ResponseDto GetItpProgressReportsByItpId(int itpId);
        public ResponseDto GetItpProgressReportById(int itpProgressReportId);
        public ResponseDto AddItpProgressReport(ItpProgressReportDto itpProgressReportDto);
        public ResponseDto EditItpProgressReport(ItpProgressReportDto itpProgressReportDto);
        public ResponseDto DeleteItpProgressReport(int itpProgressReportId);
        public ResponseDto CreateItpProgressReport(int itpId);
        public ResponseDto GetIepsForTherapist(int therapistId);
        public ResponseDto CreateItp(int iepParamedicalServiceId);
        public ResponseDto GetObjectiveByITPId(int itpId);
        public ResponseDto DeleteObjective(int objectiveId);
        public ResponseDto GetObjectiveById(int objectiveId);
        public ResponseDto EditObjectiveActivities(ItpGoalObjectiveActivitiesDto objectiveActivitiesDto);
        public ResponseDto GetActivityByObjectiveId(int objectiveId);
        public ResponseDto DeleteActivity(int activityId);

    }
}
