using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class AcadmicYear
    {
        public AcadmicYear()
        {
            Ieps = new HashSet<Iep>();
            Settings = new HashSet<Setting>();
            Terms = new HashSet<Term>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual ICollection<Iep> Ieps { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
