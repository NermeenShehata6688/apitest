using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class SkillAlowedDepartment
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? SkillId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Skill? Skill { get; set; }
    }
}
