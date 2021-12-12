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
        public ResponseDto GetIeps();
        public ResponseDto GetIepById(int iepId);
        public ResponseDto AddIep(IepDto iepDto);
        public ResponseDto EditIep(IepDto iepDto);
        public ResponseDto DeleteIep(int iepId);

        public ResponseDto GetGoals();
        public ResponseDto GetGoalById(int goalId);
        public ResponseDto AddGoal(GoalDto goalDto);
        public ResponseDto EditGoal(GoalDto goalDto);
        public ResponseDto DeleteGoal(int goalId);

        public ResponseDto GetIepParamedicalServices();
        public ResponseDto GetIepParamedicalServiceById(int iepParamedicalServiceId);
        public ResponseDto AddIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto);
        public ResponseDto DeleteIepParamedicalService(int iepParamedicalServiceId);
    }
}
