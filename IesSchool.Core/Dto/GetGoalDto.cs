using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class GetGoalDto
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }
        public int? StrandId { get; set; }
        public int? SkillId { get; set; }
        public string? CurrentLevel { get; set; }
        public string? LongTermGoal { get; set; }
        public string? ShortTermGoal { get; set; }
        public int? LongTermNumber { get; set; }
        public int? Iepid { get; set; }
        public int? ShortTermProgressNumber { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? AreaName { get; set; }
        public string? StrandName { get; set; }
        public virtual ICollection<GetObjectiveDto>? Objectives { get; set; }
    }
}
