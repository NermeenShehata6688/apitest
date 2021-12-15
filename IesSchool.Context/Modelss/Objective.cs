using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Objective
    {
        public Objective()
        {
            Activities = new HashSet<Activity>();
            ObjectiveEvaluationProcesses = new HashSet<ObjectiveEvaluationProcess>();
            ObjectiveSkills = new HashSet<ObjectiveSkill>();
        }

        public int? GoalId { get; set; }
        public int Id { get; set; }
        public string? ObjectiveNote { get; set; }
        public int? ObjectiveNumber { get; set; }
        public string? Indication { get; set; }
        public DateTime? Date { get; set; }
        public string? ResourcesRequired { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsMasterd { get; set; }
        public int? IepId { get; set; }
        public string? Entry { get; set; }
        public string? InstructionPractice { get; set; }
        public string? Evaluation { get; set; }

        public virtual Goal? Goal { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ObjectiveEvaluationProcess> ObjectiveEvaluationProcesses { get; set; }
        public virtual ICollection<ObjectiveSkill> ObjectiveSkills { get; set; }
    }
}
