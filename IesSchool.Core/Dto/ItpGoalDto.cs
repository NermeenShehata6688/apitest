using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpGoalDto
    {
        public int Id { get; set; }
        public int? ItpId { get; set; }
        public string? Goal { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public virtual ICollection<ItpGoalObjectiveDto>? ItpGoalObjectives { get; set; }
    }
}
