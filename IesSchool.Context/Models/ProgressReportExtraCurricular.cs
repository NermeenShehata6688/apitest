using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ProgressReportExtraCurricular
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ExtraCurricularId { get; set; }
        public string? Comment { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public int? IepextraCurricularId { get; set; }

        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual IepExtraCurricular? IepextraCurricular { get; set; }
        public virtual IepProgressReport? ProgressReport { get; set; }
    }
}
