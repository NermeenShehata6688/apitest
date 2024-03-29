﻿using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class AcadmicYear
    {
        public AcadmicYear()
        {
            IepProgressReports = new HashSet<IepProgressReport>();
            Ieps = new HashSet<Iep>();
            ItpProgressReports = new HashSet<ItpProgressReport>();
            Itps = new HashSet<Itp>();
            Ixps = new HashSet<Ixp>();
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

        public virtual ICollection<IepProgressReport> IepProgressReports { get; set; }
        public virtual ICollection<Iep> Ieps { get; set; }
        public virtual ICollection<ItpProgressReport> ItpProgressReports { get; set; }
        public virtual ICollection<Itp> Itps { get; set; }
        public virtual ICollection<Ixp> Ixps { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
    }
}
