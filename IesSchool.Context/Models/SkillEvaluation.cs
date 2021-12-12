using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class SkillEvaluation
    {
        public SkillEvaluation()
        {
            ObjectiveEvaluationProcesses = new HashSet<ObjectiveEvaluationProcess>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<ObjectiveEvaluationProcess> ObjectiveEvaluationProcesses { get; set; }
    }
}
