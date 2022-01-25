using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class Term
    {
        public Term()
        {
            IepProgressReports = new HashSet<IepProgressReport>();
            Ieps = new HashSet<Iep>();
            ItpProgressReports = new HashSet<ItpProgressReport>();
            Itps = new HashSet<Itp>();
            Settings = new HashSet<Setting>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AcadmicYearId { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual AcadmicYear? AcadmicYear { get; set; }
        public virtual ICollection<IepProgressReport> IepProgressReports { get; set; }
        public virtual ICollection<Iep> Ieps { get; set; }
        public virtual ICollection<ItpProgressReport> ItpProgressReports { get; set; }
        public virtual ICollection<Itp> Itps { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
    }
}
