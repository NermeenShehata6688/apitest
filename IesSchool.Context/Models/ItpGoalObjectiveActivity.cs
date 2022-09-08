using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Context.Models
{
    public partial class ItpGoalObjectiveActivity
    {
        public int Id { get; set; }
        public string? Deatils { get; set; }
        public DateTime? Date { get; set; }
        public string? CreatedBy { get; set; }
        public int? ItpGoalObjectiveId { get; set; }
        public int? Evaluation { get; set; }

        public virtual ItpGoalObjective? ItpGoalObjective { get; set; }
    }
}
