using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IItpService
    {
        public ResponseDto GetItpsHelper();
        public ResponseDto GetItps(ItpSearchDto itpSearchDto);
        public ResponseDto GetItpById(int itpId);
        public ResponseDto AddItp(ItpDto itpDto);
        public ResponseDto EditItp(ItpDto itpDto);
        public ResponseDto DeleteItp(int itpId);
        public ResponseDto ItpStatus(int itpId, int status);
        public ResponseDto ItpIsPublished(int itpId, bool isPublished);
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


    }
}
