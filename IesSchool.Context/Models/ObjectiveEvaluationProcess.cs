using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ObjectiveEvaluationProcess
    {
        public int Id { get; set; }
        public int? SkillEvaluationId { get; set; }
        public int? ObjectiveId { get; set; }

        public virtual Objective? Objective { get; set; }
        public virtual SkillEvaluation? SkillEvaluation { get; set; }
    }
}
