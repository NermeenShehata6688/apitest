using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ItpGoal
    {
        public ItpGoal()
        {
            ItpGoalObjectives = new HashSet<ItpGoalObjective>();
        }

        public int Id { get; set; }
        public int? ItpId { get; set; }
        public string? Goal { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Itp? Itp { get; set; }
        public virtual ICollection<ItpGoalObjective> ItpGoalObjectives { get; set; }
    }
}
