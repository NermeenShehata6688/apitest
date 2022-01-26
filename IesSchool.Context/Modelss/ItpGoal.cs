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

        public virtual Itp? Itp { get; set; }
        public virtual ICollection<ItpGoalObjective> ItpGoalObjectives { get; set; }
    }
}
