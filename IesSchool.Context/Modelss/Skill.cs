using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Skill
    {
        public Skill()
        {
            Goals = new HashSet<Goal>();
            ObjectiveSkills = new HashSet<ObjectiveSkill>();
            SkillAlowedDepartments = new HashSet<SkillAlowedDepartment>();
            StudentHistoricalSkills = new HashSet<StudentHistoricalSkill>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public int? Level { get; set; }
        public int? StrandId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Strand? Strand { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<ObjectiveSkill> ObjectiveSkills { get; set; }
        public virtual ICollection<SkillAlowedDepartment> SkillAlowedDepartments { get; set; }
        public virtual ICollection<StudentHistoricalSkill> StudentHistoricalSkills { get; set; }
    }
}
