using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Department
    {
        public Department()
        {
            Assistants = new HashSet<Assistant>();
            Events = new HashSet<Event>();
            SkillAlowedDepartments = new HashSet<SkillAlowedDepartment>();
            Students = new HashSet<Student>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<Assistant> Assistants { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<SkillAlowedDepartment> SkillAlowedDepartments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
