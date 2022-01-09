using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ProgressReportParamedical
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public string? Comment { get; set; }

        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual IepProgressReport? ProgressReport { get; set; }
    }
}
