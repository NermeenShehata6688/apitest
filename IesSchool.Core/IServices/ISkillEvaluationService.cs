using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ISkillEvaluationService
    {
        public ResponseDto GetSkillEvaluations();
        public ResponseDto GetSkillEvaluationById(int skillEvaluationId);
        public ResponseDto AddSkillEvaluation(SkillEvaluationDto skillEvaluationDto);
        public ResponseDto EditSkillEvaluation(SkillEvaluationDto skillEvaluationDto);
        public ResponseDto DeleteSkillEvaluation(int skillEvaluationId);
    }
}

