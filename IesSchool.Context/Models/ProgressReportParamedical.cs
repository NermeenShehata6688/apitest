using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ProgressReportParamedical
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? IepParamedicalSerciveId { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual IepParamedicalService? IepParamedicalSercive { get; set; }
        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual IepProgressReport? ProgressReport { get; set; }
    }
}
