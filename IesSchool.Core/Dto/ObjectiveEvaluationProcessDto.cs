using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ObjectiveEvaluationProcessDto
    {
        public int Id { get; set; }
        public int? SkillEvaluationId { get; set; }
        public int? ObjectiveId { get; set; }
       // public virtual SkillEvaluationDto? SkillEvaluation { get; set; }

    }
}
