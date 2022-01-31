﻿using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ExtraCurricular
    {
        public ExtraCurricular()
        {
            IepExtraCurriculars = new HashSet<IepExtraCurricular>();
            ProgressReportExtraCurriculars = new HashSet<ProgressReportExtraCurricular>();
            IxpExtraCurriculars = new HashSet<IxpExtraCurricular>();
            UserExtraCurriculars = new HashSet<UserExtraCurricular>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<IepExtraCurricular> IepExtraCurriculars { get; set; }
        public virtual ICollection<ProgressReportExtraCurricular> ProgressReportExtraCurriculars { get; set; }
        public virtual ICollection<IxpExtraCurricular> IxpExtraCurriculars { get; set; }
        public virtual ICollection<UserExtraCurricular> UserExtraCurriculars { get; set; }

    }
}
